using System.Globalization;
using System.Reflection;
using Entities;
using NPOI.XSSF.UserModel;

namespace BudgetUtilities;

/// <summary>
/// Class that manages importing data from an Excel spreadsheet and uploads it
/// to the database. Validates the Excel spreadsheet to ensure that it's formatted
/// correctly.
/// </summary>
public class DataImportUtility : IDisposable
{
    #region Fields

    /// <summary>
    /// Path to the directory where any import files will be located.
    /// </summary>
    private static readonly string ImportDirectoryPath =
        AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Import\\";

    private FileStream? fileStream;

    #endregion

    #region Properties

    /// <summary>
    /// Name of the Excel file to import from the Import directory
    /// </summary>
    private string ExcelFileName { get; init; }

    /// <summary>
    /// Dictionary containing the names of the required Excel tables mapped to
    /// the names of required columns within each table
    /// </summary>
    private Dictionary<ExcelTableAttribute, List<ExcelColumnAttribute>> ExcelSchema { get; init; }

    #endregion

    #region Methods

    /// <summary>
    /// Constructs a new instance of this class
    /// </summary>
    public DataImportUtility(string excelFileName)
    {
        ExcelFileName = excelFileName;
        ExcelSchema = GetExcelSchema();
    }

    /// <summary>
    /// Execute the data import action.
    /// Imports data from the provided Excel spreadsheet and uploads it to the SQL database through
    /// the REST API. Validates that each sheet in the Excel spreadsheet is formatted correctly.
    /// </summary>
    public void Execute()
    {
        XSSFWorkbook excelWorkbook = ImportWorkbook();
        ValidateWorkbook(excelWorkbook);
    }

    /// <summary>
    /// Imports the Excel workbook as an XSSFWorkbook. Throws an error if the ExcelFileName does not exist in the Import 
    /// directory or if the file located at the path is not a valid Excel workbook.
    /// </summary>
    /// <returns>An XSSFWorkbook representing the Excel workbook found at ExcelFileName</returns>
    public XSSFWorkbook ImportWorkbook()
    {
        // Verify that the file exists
        string fullPath = ImportDirectoryPath + ExcelFileName;
        if (!File.Exists(fullPath))
        {
            throw new Exception($"Unable to find Excel workbook in Import directory: \"{ExcelFileName}\"");
        }

        // Import the file as an XSSFWorkbook
        fileStream = new FileStream(fullPath, FileMode.Open);
        return new XSSFWorkbook(fileStream);
    }

    /// <summary>
    /// Validates the provided XSSFWorkbook. Throws an error if any sheet in the provided XSSFWorkbook does not 
    /// have the correct name, tables, or table columns. 
    /// </summary>
    /// <param name="excelWorkbook">An XSSFWorkbook representing the imported Excel workbook</param>
    public void ValidateWorkbook(XSSFWorkbook excelWorkbook)
    {
        for (int i = 0; i < excelWorkbook.NumberOfSheets; ++i)
        {
            ValidateWorksheet((XSSFSheet)excelWorkbook.GetSheetAt(i));
        }
    }

    /// <summary>
    /// Validates the provided Excel worksheet. An exception will be thrown if the worksheet is
    /// named incorrectly, doesn't have the correct set of tables, or doesn't have the correct set of table columns.
    /// </summary>
    /// <param name="worksheet">Excel worksheet to validate</param>
    public void ValidateWorksheet(XSSFSheet worksheet)
    {
        // Validate that the worksheet is named correctly
        if (!DateTime.TryParseExact(worksheet.SheetName, "yyyy-MM", null, DateTimeStyles.None, out DateTime month))
        {
            throw new Exception($"Worksheet name \"{worksheet.SheetName}\" is not a valid date in yyyy-MM format");
        }

        // Verify that all the required tables appear as expected in the worksheet
        List<XSSFTable> worksheetTables = worksheet.GetTables();
        foreach (ExcelTableAttribute expectedTable in ExcelSchema.Keys)
        {
            var actualTableName = GetActualTableName(expectedTable.TableName, month);
            XSSFTable table = worksheetTables.FirstOrDefault(worksheetTable => worksheetTable.Name == actualTableName)
                ?? throw new Exception(@$"Worksheet ""{worksheet.SheetName}"" does not contain 
                    table ""{actualTableName}""");

            List<XSSFTableColumn> columns = table.GetColumns();
            foreach (ExcelColumnAttribute expectedColumn in ExcelSchema[expectedTable])
            {
                XSSFTableColumn column = columns.FirstOrDefault(column => column.Name == expectedColumn.ColumnName)
                    ?? throw new Exception(@$"Worksheet ""{worksheet.SheetName}"" table 
                        ""{actualTableName}"" does not contain column ""{expectedColumn.ColumnName}""");
            }
        }

        // Verify that the worksheet doesn't contain any extra tables
        if (worksheetTables.Count != ExcelSchema.Keys.Count)
        {
            throw new Exception($"Worksheet \"{worksheet.SheetName}\" contains extra Excel tables");
        }
    }

    /// <summary>
    /// Gets the actual table name as it should appear for the given month. Table names in the Excel workbook must be
    /// uniquely identified by appending the sheet's month to the end of the expected table name.
    /// </summary>
    /// <param name="expectedTableName">Expected table name</param>
    /// <param name="month">DateTime representing the month of the current worksheet</param>
    /// <returns>The actual table name that should appear in the worksheet for the given month</returns>
    private static string GetActualTableName(string expectedTableName, DateTime month) =>
        expectedTableName + "." + month.ToString("yyyy.MM");

    /// <summary>
    /// Reflects over the Entities assembly to determine what Excel tables and columns are required.
    /// </summary> 
    /// <returns>A Dictionary mapping the required table names to their list of required columns</returns>
    private static Dictionary<ExcelTableAttribute, List<ExcelColumnAttribute>> GetExcelSchema()
    {
        var excelSchema = new Dictionary<ExcelTableAttribute, List<ExcelColumnAttribute>>();

        Assembly entityAssembly = Assembly.GetAssembly(typeof(ExcelTableAttribute))
            ?? throw new Exception("Unable to access Entities assembly");
        foreach (Type type in entityAssembly.GetTypes())
        {
            // Build the list of column attributes that exist under this class
            List<ExcelColumnAttribute> columnAttributes = type.GetProperties()
                .SelectMany(property => Attribute.GetCustomAttributes(property, typeof(ExcelColumnAttribute)))
                .Where(attribute => attribute is ExcelColumnAttribute)
                .Select(attribute => (ExcelColumnAttribute)attribute)
                .ToList();

            // For each table attribute that exists on this class, add the list of column attributes to the
            // list of required columns for that table
            foreach (Attribute attribute in Attribute.GetCustomAttributes(type, typeof(ExcelTableAttribute)))
            {
                var tableAttribute = attribute as ExcelTableAttribute ?? throw new InvalidOperationException();
                if (excelSchema.ContainsKey(tableAttribute))
                {
                    excelSchema[tableAttribute] = excelSchema[tableAttribute].Concat(columnAttributes).ToList();
                }
                else
                {
                    excelSchema.Add(tableAttribute, columnAttributes);
                }
            }
        }

        return excelSchema;
    }

    #region IDisposable

    /// <inheritdoc/>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        fileStream?.Dispose();
    }

    #endregion

    #endregion
}
