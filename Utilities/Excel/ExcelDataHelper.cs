using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Models.Attributes;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel;

/// <summary>
/// Helper class containing various methods to handle data coming from an Excel table.
/// </summary>
public static class ExcelDataHelper
{
    #region Properties

    /// <summary>
    /// Sheet name containing setup data
    /// </summary>
    public const string SetupSheetName = "Setup";

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets an IEnumerable of the worksheets in the provided Excel workbook
    /// </summary>
    /// <param name="excelWorkbook">Current Excel workbook</param>
    /// <returns>List of Excel sheets found in the provided workbook</returns>
    public static IEnumerable<XSSFSheet> GetWorksheets(XSSFWorkbook excelWorkbook) =>
        from i in Enumerable.Range(0, excelWorkbook.NumberOfSheets) select (XSSFSheet)excelWorkbook.GetSheetAt(i);

    /// <summary>
    /// Determines if the provided sheet name is valid for an Excel worksheet containing setup data.
    /// </summary>
    /// <param name="sheetName">Sheet name to validate</param>
    /// <returns>True if the sheet name is valid for a sheet with setup data, false otherwise</returns>
    public static bool IsValidSetupSheetName(string sheetName) => sheetName == SetupSheetName;

    /// <summary>
    /// Determines if the provided sheet name is valid for an Excel worksheet containing monthly data.
    /// </summary>
    /// <param name="sheetName">Sheet name to validate</param>
    /// <param name="month">If the sheet name is valid for monthly data, a DateTime representing the first
    /// day of the month that the sheet represents</param>
    /// <returns>True if the sheet name is valid for a sheet with monthly data, false otherwise</returns>
    public static bool IsValidMonthlySheetName(string sheetName, [NotNullWhen(true)] out DateTime? month)
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
    /// Gets the Excel worksheet containing setup data
    /// </summary>
    /// <param name="excelWorkbook">Current Excel workbook</param>
    /// <returns>Excel worksheet containing setup data</returns>
    public static XSSFSheet GetSetupSheet(XSSFWorkbook excelWorkbook) =>
        GetWorksheets(excelWorkbook).Single(sheet => IsValidSetupSheetName(sheet.SheetName));

    /// <summary>
    /// Gets the list of Excel worksheets containing monthly data
    /// </summary>
    /// <param name="excelWorkbook">Current Excel workbook</param>
    /// <returns>List of monthly Excel worksheets and their associated months</returns>
    public static IEnumerable<(XSSFSheet sheet, DateTime month)> GetMonthlySheets(XSSFWorkbook excelWorkbook) =>
        GetWorksheets(excelWorkbook).Select(worksheet =>
        {
            IsValidMonthlySheetName(worksheet.SheetName, out DateTime? month);
            return (sheet: worksheet, month: month ?? DateTime.MinValue);
        }).Where(tuple => tuple.month != DateTime.MinValue);

    /// <summary>
    /// Given a table name template, determines the table name that will actually appear in the given sheet
    /// </summary>
    /// <param name="worksheetName">Name of the Excel worksheet for this table</param>
    /// <param name="tableName">Table name template string</param>
    /// <returns>Table name as it should appear in the provided worksheet</returns>
    public static string GetTableName(string worksheetName, string tableName)
    {
        if (IsValidMonthlySheetName(worksheetName, out DateTime? month))
        {
            tableName = tableName.Replace("{Month}", "{0}");
            tableName = string.Format(tableName, month.Value.ToString("yyyy.MM"));
        }
        return tableName;
    }

    /// <summary>
    /// Determines the table name that contains data for provided combination of model type, parent type, and worksheet
    /// </summary>
    /// <param name="sheet">Excel worksheet containing data for the given model type</param>
    /// <param name="modelType">Model type of interest</param>
    /// <param name="parentType">If provided model type is a child model, the parent model of the child.</param>
    /// <returns>Table name should contain data for the provided model in the provided worksheet</returns>
    public static string GetTableNameForType(XSSFSheet sheet, Type modelType, Type? parentType = null)
    {
        string tableName = Attribute.GetCustomAttributes(modelType, typeof(ExcelTableAttribute))
            .OfType<ExcelTableAttribute>()
            .Where(attribute => parentType == null || attribute.ParentType == parentType)
            .Single().TableName;
        return GetTableName(sheet.SheetName, tableName);
    }

    /// <summary>
    /// Given a PropertyInfo for a model property, determines the Excel column name that corresponds to this property
    /// </summary>
    /// <param name="property">PropertyInfo object for a model property</param>
    /// <returns>Excel column name that corresponds to this property</returns>
    public static string GetColumnNameForProperty(PropertyInfo property) =>
        (Attribute.GetCustomAttribute(property, typeof(ExcelColumnAttribute)) as ExcelColumnAttribute)?.ColumnName ??
            throw new Exception($"Property \"{property.Name}\" on model \"{nameof(property.DeclaringType)}\" is " +
                "missing the Excel Column attribute");

    /// <summary>
    /// Determines the Excel column name that corresponds to the provided property name on the provided model type
    /// </summary>
    /// <param name="modelType">Type of the model</param>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>Excel column name that corresponds to this property on the provided model</returns>
    public static string GetColumnNameForProperty(Type modelType, string propertyName) =>
        GetColumnNameForProperty(modelType.GetProperties().Single(property => property.Name == propertyName));

    /// <summary>
    /// Given an Excel worksheet, gets the table on that worksheet that contains data for the
    /// given model.
    /// </summary>
    /// <param name="sheet">Excel worksheet containing model data</param>
    /// <param name="modelType">Type of the model</param>
    /// <param name="parentType">If the provided model type is a child model, the parent model of the child</param>
    /// <returns>Excel table on the provided worksheet for the model type, or null if one isn't found</returns>
    public static XSSFTable? GetTableForType(XSSFSheet sheet, Type modelType, Type? parentType = null) =>
        sheet.GetTables().FirstOrDefault(table => table.Name == GetTableNameForType(sheet, modelType, parentType));

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
    /// Gets the highest row index in the current table
    /// </summary>
    /// <param name="table">Current Excel table</param>
    /// <returns>Highest row index in the provided table (number of rows minus one)</returns>
    public static int GetMaximumRowIndex(XSSFTable table) => table.RowCount - 2;

    #endregion
}