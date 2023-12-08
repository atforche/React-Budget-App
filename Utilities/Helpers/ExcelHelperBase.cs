using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Utilities.Helpers;

/// <summary>
/// Base class for common functionality shared by all Excel Helper classes
/// </summary>
/// <remarks>
/// Constructs a new instance of this class
/// </remarks>
public class ExcelHelperBase(XSSFWorkbook excelWorkbook)
{
    #region Properties

    /// <summary>
    /// Sheet name containing setup data
    /// </summary>
    protected const string SetupSheetName = "Setup";

    /// <summary>
    /// Excel workbook
    /// </summary>
    protected XSSFWorkbook Workbook { get; init; } = excelWorkbook;

    /// <summary>
    /// List of errors found during validation
    /// </summary>
    protected List<Exception> Exceptions { get; init; } = [];

    #endregion

    #region Methods

    /// <summary>
    /// Gets an IEnumerable of the worksheets in the Excel workbook
    /// </summary>
    /// <returns></returns>
    public IEnumerable<XSSFSheet> GetWorksheets() =>
        from i in Enumerable.Range(0, Workbook.NumberOfSheets) select (XSSFSheet)Workbook.GetSheetAt(i);

    /// <summary>
    /// Gets the specified column and row from the provided Excel table
    /// </summary>
    /// <param name="table">Excel table containing the cell</param>
    /// <param name="columnName">Excel column name containing the cell</param>
    /// <param name="index">Excel table index containing the cell</param>
    /// <returns>An XSSFCell object representing the cell in the Excel table</returns>
    public static XSSFCell GetTableCell(XSSFTable table, string columnName, int index)
    {
        XSSFTableColumn column = table.GetColumns().First(column => column.Name == columnName);
        int row = table.StartRowIndex + 1 + index;
        int col = table.StartColIndex + column.ColumnIndex;
        return (XSSFCell)table.GetXSSFSheet().GetRow(row).GetCell(col);
    }

    /// <summary>
    /// Gets the specified column and row from the provided Excel table. If the specified
    /// cell is blank, null is returned.
    /// </summary>
    /// <param name="table">Excel table containing the cell</param>
    /// <param name="columnName">Excel column name containing the cell</param>
    /// <param name="index">Excel table index containing the cell</param>
    /// <returns>An XSSFCell object representing the cell in the Excel table, or null if the cell is blank</returns>
    public static XSSFCell? GetTableCellOrNull(XSSFTable table, string columnName, int index)
    {
        XSSFCell cell = GetTableCell(table, columnName, index);
        return cell.CellType == CellType.Blank ? null : cell;
    }

    /// <summary>
    /// Determines if the provided sheet name is valid for an Excel worksheet containing setup data.
    /// </summary>
    /// <param name="sheetName">Sheet name to validate</param>
    /// <returns>True if the sheet name is valid for a sheet with setup data, false otherwise</returns>
    protected static bool IsValidSetupSheetName(string sheetName) => sheetName == SetupSheetName;

    /// <summary>
    /// Determines if the provided sheet name is valid for an Excel worksheet containing monthly data.
    /// </summary>
    /// <param name="sheetName">Sheet name to validate</param>
    /// <param name="month">If the sheet name is valid for monthly data, a DateTime representing the first
    /// day of the month that the sheet represents</param>
    /// <returns>True if the sheet name is valid for a sheet with monthly data, false otherwise</returns>
    protected static bool IsValidMonthlySheetName(string sheetName, [NotNullWhen(true)] out DateTime? month)
    {
        month = null;
        if (DateTime.TryParseExact(sheetName, "yyyy-MM", null, DateTimeStyles.None, out DateTime parsedMonth))
        {
            month = parsedMonth;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets the sheet containing setup data
    /// </summary>
    /// <returns>An XSSFSheet object representing the sheet with setup data</returns>
    protected XSSFSheet GetSetupSheet() =>
        GetWorksheets().Where(sheet => IsValidSetupSheetName(sheet.SheetName)).First();

    /// <summary>
    /// Gets the list of sheets containing monthly data
    /// </summary>
    /// <returns>An IEnumerable of XSSFSheets representing the sheets with monthly data</returns>
    protected IEnumerable<(XSSFSheet sheet, DateTime month)> GetMonthlySheets() =>
        GetWorksheets().Select(worksheet =>
        {
            IsValidMonthlySheetName(worksheet.SheetName, out DateTime? month);
            return (sheet: worksheet, month: month ?? DateTime.MinValue);
        }).Where(tuple => tuple.month != DateTime.MinValue);

    /// <summary>
    /// Gets an IEnumerable of the Types exported by the Models assembly
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the Models assembly cannot be found</exception>
    protected static IEnumerable<Type> GetModelTypes()
    {
        Assembly modelsAssembly = Assembly.GetAssembly(typeof(ExcelTableAttribute))
            ?? throw new Exception("Unable to access Models assembly");
        return modelsAssembly.GetTypes().Where(type => type.Namespace == nameof(Models));
    }

    /// <summary>
    /// Gets the Excel Table attributes for the provided type.
    /// </summary>
    protected static IEnumerable<ExcelTableAttribute> GetExcelTables(Type type) =>
        Attribute.GetCustomAttributes(type, typeof(ExcelTableAttribute)).OfType<ExcelTableAttribute>();

    /// <summary>
    /// Gets the actual table name as it should appear for the given month. Table names in the Excel workbook must be
    /// uniquely identified by appending the sheet's month to the end of the expected table name.
    /// </summary>
    /// <param name="expectedTableName">Expected table name</param>
    /// <param name="month">DateTime representing the month of the current worksheet</param>
    /// <returns>The actual table name that should appear in the worksheet for the given month</returns>
    protected static string GetActualTableName(string expectedTableName, DateTime? month)
    {
        if (month != null)
        {
            string formatString = expectedTableName.Replace("{Month}", "{0}");
            return string.Format(formatString, month.Value.ToString("yyyy.MM"));
        }
        return expectedTableName;
    }

    /// <summary>
    /// Gets the Excel Table name for the provided type and parent type
    /// </summary>
    /// <param name="type">Type whose data is stored in the table</param>
    /// <param name="parentType">If the type appears on multiple tables, the parent type to disambiguate
    /// which table name to retrieve</param>
    protected static string GetExcelTableName(Type type, DateTime? month = null, Type? parentType = null) =>
        GetActualTableName(GetExcelTables(type).First(table => table.ParentType == parentType).TableName, month);

    /// <summary>
    /// Gets the Excel Column attributes for the provided type.
    /// </summary>
    protected static IEnumerable<ExcelColumnAttribute> GetExcelColumns(Type type) =>
        type.GetProperties()
            .SelectMany(property => Attribute.GetCustomAttributes(property, typeof(ExcelColumnAttribute)))
            .OfType<ExcelColumnAttribute>();

    /// <summary>
    /// Gets the Excel Column name for a particular property
    /// </summary>
    /// <param name="type">Type the property is on</param>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>The Excel Column name for the provided property</returns>
    /// <exception cref="Exception"></exception>
    protected static string GetExcelColumnNameFromProperty(Type type, string propertyName)
    {
        PropertyInfo property = type.GetProperty(propertyName)
            ?? throw new Exception($"Property \"{propertyName}\" is not value for type \"{type}\"");
        return (Attribute.GetCustomAttribute(property, typeof(ExcelColumnAttribute)) as ExcelColumnAttribute)?.ColumnName
            ?? throw new Exception($"Property \"{propertyName}\" on type \"{type}\" does not have an Excel Column attribute");
    }

    /// <summary>
    /// Throws an exception that outlines all the errors encountered by the validator
    /// </summary>
    protected void ThrowExceptions()
    {
        if (Exceptions.Count > 0)
        {
            List<string> outputStringList = Exceptions.Select(exception => exception.Message).ToList();
            outputStringList.Insert(0, "The following errors were encountered while validating the workbook:");
            throw new Exception(string.Join("\n", outputStringList));
        }
    }

    #endregion
}