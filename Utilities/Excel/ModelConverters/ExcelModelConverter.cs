using System.Reflection;
using Models.Attributes;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Base class containing shared functionality for all Excel model converters.
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
/// <param name="parentType">If this converter deals with child models, the parent type of this model converter</param>
public abstract class ExcelModelConverter<TModelInterface, TModelCreateRequestInterface>(
    XSSFSheet worksheet,
    Type? parentType = null)
    : IExcelModelConverter
    where TModelInterface : class
    where TModelCreateRequestInterface : class
{
    #region Properties

    /// <summary>
    /// Dictionary mapping property names to the lambda used to build that property's value from an Excel column
    /// </summary>
    protected virtual Dictionary<string, Func<XSSFCell, object?>> ColumnConverters { get; } = [];

    /// <summary>
    /// Dictionary mapping property names to a child model converter that build that property's value
    /// </summary>
    protected virtual Dictionary<string, IExcelModelConverter> ChildModelConverters { get; } = [];

    /// <summary>
    /// Excel worksheet containing model data
    /// </summary>
    protected XSSFSheet Worksheet { get; } = worksheet;

    /// <summary>
    /// Current row index in the Excel table for this converter
    /// </summary> 
    protected int CurrentIndex { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    /// Gets a default create request for the current model
    /// </summary>
    public abstract TModelCreateRequestInterface GetDefaultCreateRequest();

    /// <summary>
    /// Given the child model type and current Excel table, determines the range of rows in the Excel table that 
    /// should contain child models of the given type for the current parent model. Assumes the parent model
    /// is the model found at the current index. 
    /// </summary>
    /// <param name="childType">Type of the child model</param>
    /// <param name="table">Current Excel table</param>
    /// <returns>The range of rows that contain child models of the given type for the current parent, or null</returns>
    protected virtual ExcelRowRange? GetChildModelIndexes(Type childType, XSSFTable table) => null;

    /// <summary>
    /// Converts data in the provided Excel worksheet into a list of data models
    /// </summary>
    /// <param name="bounds">If provided, a range that limits the rows that models will be converted from</param>
    /// <param name="exceptions">Exceptions encountered during the conversion process</param>
    /// <returns>An IEnumerable of objects containing the converted models</returns>
    public IEnumerable<TModelCreateRequestInterface> ConvertExcelToModels(
        ExcelRowRange? bounds,
        out IEnumerable<Exception> exceptions)
    {
        List<Exception> exceptionsList = [];
        var convertedModels = new List<TModelCreateRequestInterface>();

        // Grab the model type's table from the Excel worksheet
        XSSFTable? modelTable = ExcelDataHelper.GetTableForType(Worksheet, typeof(TModelInterface), parentType);
        if (modelTable == null)
        {
            exceptionsList.Add(new Exception("Unable to find table " +
                $"\"{ExcelDataHelper.GetTableNameForType(Worksheet, typeof(TModelInterface))}\" in worksheet " +
                $"\"{Worksheet.SheetName}\""));
            exceptions = exceptionsList;
            return Enumerable.Empty<TModelCreateRequestInterface>();
        }
        // Convert a model for each child row of the given parent row
        CurrentIndex = bounds?.StartingIndex ?? 0;
        int endingIndex = bounds?.EndingIndex ?? ExcelDataHelper.GetMaximumRowIndex(modelTable);
        while (CurrentIndex <= endingIndex)
        {
            if (TryConvertModelFromRow(modelTable, exceptionsList, out TModelCreateRequestInterface model) &&
                model != GetDefaultCreateRequest())
            {
                convertedModels.Add(model);
            }
        }
        exceptions = exceptionsList;
        return convertedModels;
    }

    /// <inheritdoc/>
    IEnumerable<object> IExcelModelConverter.ConvertExcelToModels(
        ExcelRowRange? bounds,
        out IEnumerable<Exception> exceptions) => ConvertExcelToModels(bounds, out exceptions);

    /// <inheritdoc/>
    public void ConvertModelsToExcel(
        IEnumerable<object> models,
        XSSFTable table,
        out IEnumerable<Exception> exceptions) =>
        throw new NotImplementedException();

    /// <summary>
    /// Attempts to convert data in the current Excel row into a model creation request
    /// </summary>
    /// <param name="table">Excel table containing model data</param>
    /// <param name="exceptionList">List of exceptions encountered during conversion</param>
    /// <param name="model">Model creation request converted from the Excel row</param>
    /// <returns>True if the model was converted with no errors, false otherwise</returns>
    private bool TryConvertModelFromRow(
        XSSFTable table,
        List<Exception> exceptionList,
        out TModelCreateRequestInterface model)
    {
        bool success = true;
        model = GetDefaultCreateRequest();
        var consumedRows = new ExcelRowRange(CurrentIndex, CurrentIndex);

        // Loop through the properties on the interface model
        foreach (PropertyInfo interfaceProperty in typeof(TModelInterface).GetProperties())
        {
            try
            {
                // Check if this property has a custom attribute that maps to Excel data
                GetPropertyInformation(interfaceProperty,
                    out ExcelColumnAttribute? columnAttribute,
                    out ExcelChildModelAttribute? childModelAttribute,
                    out PropertyInfo createRequestProperty);
                if (columnAttribute == null && childModelAttribute == null)
                {
                    continue;
                }
                // If the property has a cell converter, grab the cell and invoke the method
                if (columnAttribute != null)
                {
                    Func<XSSFCell, object?> converter = ColumnConverters[createRequestProperty.Name];
                    string columnName = ExcelDataHelper.GetColumnNameForProperty(interfaceProperty);
                    XSSFCell cell = ExcelDataHelper.GetTableCell(table, columnName, CurrentIndex);
                    createRequestProperty.SetValue(model, converter.Invoke(cell));
                }
                // If the property has a child model converter, invoke the converter using the current table
                else if (childModelAttribute != null)
                {
                    ExcelRowRange? childModelRowRange = GetChildModelIndexes(
                        childModelAttribute.IsOneToOne
                            ? interfaceProperty.PropertyType
                            : interfaceProperty.PropertyType.GenericTypeArguments.First(),
                        table);
                    if (childModelRowRange != null)
                    {
                        IExcelModelConverter childConverter = ChildModelConverters[createRequestProperty.Name];
                        IEnumerable<object> convertedChildren = childConverter.ConvertExcelToModels(
                            childModelRowRange,
                            out IEnumerable<Exception> childExceptions);
                        createRequestProperty.SetValue(model,
                            childModelAttribute.IsOneToOne
                                ? convertedChildren.Single()
                                : convertedChildren);
                        exceptionList.AddRange(childExceptions);
                        consumedRows = ExcelRowRange.Union(consumedRows, childModelRowRange, true);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                exceptionList.Add(e);
            }
        }
        IncrementCurrentIndex(consumedRows);
        return success;
    }

    /// <summary>
    /// Given a PropertyInfo object representing a property on the model interface, determines any relevant attributes
    /// and related properties on the creation model for the given property
    /// </summary>
    /// <param name="interfacePropertyInfo">PropertyInfo representing a property on the model's interface type</param>
    /// <param name="columnAttribute">ExcelColumnAttribute on the provided property, if one exists</param>
    /// <param name="childModelAttribute">ExcelChildModelAttribute on the provided property, if one exists</param>
    /// <param name="createRequestPropertyInfo">PropertyInfo representing the related property on the model's 
    /// creation request type</param>
    private void GetPropertyInformation(
        PropertyInfo interfacePropertyInfo,
        out ExcelColumnAttribute? columnAttribute,
        out ExcelChildModelAttribute? childModelAttribute,
        out PropertyInfo createRequestPropertyInfo)
    {
        // Grab any relevant attributes off the property
        IEnumerable<Attribute> customAttributes = Attribute.GetCustomAttributes(interfacePropertyInfo);
        columnAttribute = customAttributes.OfType<ExcelColumnAttribute>().SingleOrDefault();
        childModelAttribute = customAttributes.OfType<ExcelChildModelAttribute>().SingleOrDefault();

        // Grab the corresponding property on the create request model
        createRequestPropertyInfo = typeof(TModelCreateRequestInterface).GetProperty(interfacePropertyInfo.Name) ??
            throw new Exception($"Model \"{nameof(TModelCreateRequestInterface)}\" does not have a matching " +
                $"property \"{interfacePropertyInfo.Name}\" found on the main model \"{nameof(TModelInterface)}\"");
    }

    /// <summary>
    /// Child converters may consume additional rows beyond just the current row. After all the property converters
    /// have been invoked, increment the current index beyond the consumed rows
    /// </summary>
    /// <param name="allConsumedRows">All rows that were consumed by property converters</param>
    private void IncrementCurrentIndex(ExcelRowRange allConsumedRows)
    {
        if (allConsumedRows.StartingIndex == CurrentIndex && allConsumedRows.EndingIndex == CurrentIndex)
        {
            CurrentIndex++;
        }
        else
        {
            CurrentIndex = allConsumedRows.EndingIndex + 1;
        }
    }

    #endregion
}