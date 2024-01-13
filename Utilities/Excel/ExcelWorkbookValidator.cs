using System.Reflection;
using Models.Attributes;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel;

/// <summary>
/// Class that validates an Excel Workbook to ensure it is structured correctly for import or export.
/// This class does not perform any domain-specific validation.
/// </summary>
public class ExcelWorkbookValidator(XSSFWorkbook excelWorkbook)
{
    #region Fields

    /// <summary>
    /// Dictionary containing the names of the required Excel tables mapped to 
    /// the names of required columns within each table
    /// </summary>
    private static readonly Dictionary<ExcelTableAttribute, List<ExcelColumnAttribute>> excelSchema = GetExcelSchema();

    /// <summary>
    /// List of exceptions encountered during validation
    /// </summary>
    private readonly List<Exception> exceptionList = [];

    #endregion

    #region Methods

    /// <summary>
    /// Validates the current Excel workbook. Logs an error if any sheet in the workbook does not 
    /// have the correct name, tables, or table columns. 
    /// </summary>
    /// <param name="exceptions">Exceptions encountered during validation</param>
    public void Validate(out IEnumerable<Exception> exceptions)
    {
        exceptionList.Clear();
        if (excelWorkbook.NumberOfSheets < 2)
        {
            exceptionList.Add(new Exception("A valid workbook must have at least two sheets"));
        }
        if (ValidateWorksheetNames())
        {
            foreach (XSSFSheet sheet in ExcelDataHelper.GetWorksheets(excelWorkbook))
            {
                ValidateWorksheet(sheet);
            }
        }
        exceptions = exceptionList;
    }

    /// <summary>
    /// Validates the names of the Excel worksheets. An exception will be added if the 
    /// worksheets aren't named correctly. A valid workbook has a single worksheet with setup data 
    /// and the remaining worksheets are all monthly. 
    /// </summary>
    /// <returns>True if all the worksheet names are valid, false otherwise</returns>
    private bool ValidateWorksheetNames()
    {
        // Get the list of worksheet names in the current workbook
        List<string> worksheetNames = ExcelDataHelper.GetWorksheets(excelWorkbook)
            .Select(sheet => sheet.SheetName).ToList();

        // Validate the the workbook has exactly one setup worksheet
        List<string> setupSheetNames = worksheetNames.Where(ExcelDataHelper.IsValidSetupSheetName).ToList();
        if (setupSheetNames.Count != 1)
        {
            exceptionList.Add(new Exception($"Workbook must have exactly one setup worksheet named " +
                $"\"{ExcelDataHelper.SetupSheetName}\""));
            return false;
        }

        // Validate that all the remaining worksheets are valid monthly sheets
        IEnumerable<string> monthlySheetNames = worksheetNames.Where(name =>
            ExcelDataHelper.IsValidMonthlySheetName(name, out _));
        List<string> invalidWorksheetNames = worksheetNames.Except(setupSheetNames).Except(monthlySheetNames).ToList();
        foreach (string invalidWorksheetName in invalidWorksheetNames)
        {
            exceptionList.Add(new Exception($"Workbook has an invalid sheet name \"{invalidWorksheetName}\""));
        }
        return invalidWorksheetNames.Count == 0;
    }

    /// <summary>
    /// Validates an Excel worksheet. An exception will be added if the worksheet 
    /// doesn't have the correct set of tables and columns.
    /// </summary>
    /// <param name="worksheet">Excel worksheet to validate</param>
    private void ValidateWorksheet(XSSFSheet worksheet)
    {
        // Determine if the provided sheet is a monthly sheet
        bool isMonthlySheet = ExcelDataHelper.IsValidMonthlySheetName(worksheet.SheetName, out _);

        // Verify that all the required tables appear as expected in the worksheet
        List<XSSFTable> worksheetTables = worksheet.GetTables();
        foreach (ExcelTableAttribute expectedTable in
            excelSchema.Keys.Where(table => table.IsSetup != isMonthlySheet))
        {
            var actualTableName = ExcelDataHelper.GetTableName(worksheet.SheetName, expectedTable.TableName);
            XSSFTable? table = worksheetTables.FirstOrDefault(worksheetTable => worksheetTable.Name == actualTableName);
            if (table == null)
            {
                exceptionList.Add(new Exception(
                    @$"Worksheet ""{worksheet.SheetName}"" does not contain table ""{actualTableName}"""));
                continue;
            }

            // Verify that all the required columns appear as expected in each table
            List<XSSFTableColumn> columns = table.GetColumns();
            foreach (ExcelColumnAttribute expectedColumn in excelSchema[expectedTable])
            {
                XSSFTableColumn? column = columns.FirstOrDefault(column => column.Name == expectedColumn.ColumnName);
                if (column == null)
                {
                    exceptionList.Add(new Exception(@$"Worksheet ""{worksheet.SheetName}"" table " +
                        @$"""{actualTableName}"" does not contain column ""{expectedColumn.ColumnName}"""));
                    continue;
                }

                // Verify that all the cells in the column have the valid data type
                ValidateColumnCells(expectedColumn, table);
            }
        }

        // Verify that the worksheet doesn't contain any extra tables
        foreach (XSSFTable extraTable in worksheetTables.Where(table =>
            !excelSchema.Keys.Select(key => key.TableName)
            .Any(tableName => ExcelDataHelper.GetTableName(worksheet.SheetName, tableName) == table.Name)))
        {
            exceptionList.Add(new Exception(
                $"Worksheet \"{worksheet.SheetName}\" contains extra table \"{extraTable.Name}\""));
        }
    }

    /// <summary>
    /// Validates that all the cells in the provided column have the correct cell type. An exception will be added
    /// if a cell doesn't have the correct type as outlined by the attribute.
    /// </summary>
    /// <param name="columnAttribute">Column Attribute specifying the expected cell type</param>
    /// <param name="table">Excel table the column falls under</param>
    private void ValidateColumnCells(ExcelColumnAttribute columnAttribute, XSSFTable table)
    {
        for (int i = 0; i < table.RowCount - 1; ++i)
        {
            XSSFCell cell = ExcelDataHelper.GetTableCell(table, columnAttribute.ColumnName, i);
            if (cell.CellType == CellType.Blank)
            {
                if (!columnAttribute.IsBlankAllowed)
                {
                    exceptionList.Add(new Exception($"Table \"{table.Name}\" has an illegal blank cell in Column " +
                        $"\"{columnAttribute.ColumnName}\" at Row {i + 1}"));
                }
            }
            else if (cell.CellType != GetExpectedCellType(columnAttribute))
            {
                exceptionList.Add(new Exception($"Table \"{table.Name}\" has an incorrect cell type of " +
                    $"\"{cell.CellType}\" in Column \"{columnAttribute.ColumnName}\" at Row {i + 1}"));
            }
        }
    }

    /// <summary>
    /// Translates the Column Attribute to the expected Cell Type that all cells in the column must adhere to
    /// </summary>
    /// <param name="columnAttribute">Column Attribute to translate</param>
    private CellType GetExpectedCellType(ExcelColumnAttribute columnAttribute)
    {
        var cellTypeMapping = new Dictionary<Type, CellType>()
        {
            {typeof(string), CellType.String},
            {typeof(decimal), CellType.Numeric},
            {typeof(DateTime), CellType.Numeric},
            {typeof(bool), CellType.Boolean}
        };
        if (!cellTypeMapping.TryGetValue(columnAttribute.ColumnType, out CellType cellType))
        {
            exceptionList.Add(new Exception($"Type \"{columnAttribute.ColumnType}\" is not mapped to a Cell Type"));
            return CellType.Unknown;
        }
        return cellType;
    }

    /// <summary>
    /// Reflects over the Models assembly to determine what Excel tables and columns are required.
    /// </summary> 
    /// <returns>A Dictionary mapping the required table names to their list of required columns</returns>
    private static Dictionary<ExcelTableAttribute, List<ExcelColumnAttribute>> GetExcelSchema()
    {
        var excelSchema = new Dictionary<ExcelTableAttribute, HashSet<ExcelColumnAttribute>>();
        foreach (Type type in GetModelTypes())
        {
            // For each table attribute that exists on this class, add the list of column attributes to the
            // list of required columns for that table
            foreach (ExcelTableAttribute tableAttribute in GetExcelTables(type))
            {
                // Build the list of column attributes that exist under this class
                HashSet<ExcelColumnAttribute> columnAttributes = GetExcelColumns(type).ToHashSet();
                if (excelSchema.TryGetValue(tableAttribute,
                        out HashSet<ExcelColumnAttribute>? existingColumnAttributes))
                {
                    excelSchema[tableAttribute].UnionWith(columnAttributes);
                }
                else
                {
                    excelSchema.Add(tableAttribute, columnAttributes);
                }
            }
        }
        // Now that there are no duplicate columns, convert the hash sets to lists
        return excelSchema.ToDictionary(grouping => grouping.Key, grouping => grouping.Value.ToList());
    }

    /// <summary>
    /// Gets an IEnumerable of the Types exported by the Models assembly
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the Models assembly cannot be found</exception>
    private static IEnumerable<Type> GetModelTypes()
    {
        Assembly modelsAssembly = Assembly.GetAssembly(typeof(ExcelTableAttribute))
            ?? throw new Exception("Unable to access Models assembly");
        return modelsAssembly.GetTypes().Where(type => type.Namespace == nameof(Models));
    }

    /// <summary>
    /// Gets the Excel Table attributes for the provided type.
    /// </summary>
    /// <param name="type">Provided model type</param>
    /// <returns>List of ExcelTableAttributes found on the provided type</returns>
    private static IEnumerable<ExcelTableAttribute> GetExcelTables(Type type) =>
        Attribute.GetCustomAttributes(type, typeof(ExcelTableAttribute)).OfType<ExcelTableAttribute>();

    /// <summary>
    /// Gets the Excel Column attributes for the provided type.
    /// </summary>
    /// <param name="type">Provided model type</param>
    /// <returns>List of ExcelColumnAttributes found on the provided type</returns>
    private static IEnumerable<ExcelColumnAttribute> GetExcelColumns(Type type) =>
        type.GetProperties()
            .SelectMany(property => Attribute.GetCustomAttributes(property, typeof(ExcelColumnAttribute)))
            .OfType<ExcelColumnAttribute>();

    #endregion
}