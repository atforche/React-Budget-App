using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Entities;
using NPOI.XSSF.UserModel;

namespace CommandLine;

/// <summary>
/// Class that manages importing data from an Excel spreadsheet and uploads it
/// to the database. Validates the Excel spreadsheet to ensure that it's formatted
/// correctly.
/// </summary>
public class DataImporter : IDisposable
{
    #region Fields

    private FileStream? fileStream;

    #endregion

    #region Properties

    /// <summary>
    /// Path to the directory where any import files will be located.
    /// </summary>
    private static readonly string ImportDirectoryPath =
        AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Import\\";

    /// <summary>
    /// Dictionary containing the names of the required Excel tables mapped to
    /// the names of required columns within the table
    /// </summary>
    private Dictionary<ExcelTableAttribute, List<ExcelColumnAttribute>> ExcelSchema;

    #endregion

    #region Methods

    /// <summary>
    /// Constructs a new instance of this class
    /// </summary>
    public DataImporter()
    {
        ExcelSchema = GetExcelSchema();
    }

    /// <summary>
    /// Execute the data import action.
    /// Imports data from the provided Excel spreadsheet and uploads it to the SQL database through
    /// the REST API. Validates that each sheet in the Excel spreadsheet is formatted correctly.
    /// </summary>
    /// <param name="excelFileName">Name of the Excel workbook in the Import directory</param>
    public void Execute(string excelFileName)
    {
        ValidateWorkbook(excelFileName, out XSSFWorkbook? excelWorkbook);
    }

    /// <summary>
    /// Validates the provided Excel file. If the file is determined to have a valid format,
    /// it will also return the imported workbook as an out parameter.
    /// </summary>
    /// <param name="excelFileName">Name of the Excel workbook in the Import directory</param>
    /// <param name="excelWorkbook">If the provided workbook is valid, a XSSFWorkbook object that
    /// can be used to interact with the workbook. Null otherwise.</param>
    public void ValidateWorkbook(
        string excelFileName,
        [NotNullWhen(true)] out XSSFWorkbook? excelWorkbook
    )
    {
        excelWorkbook = null;

        // Verify that the file exists
        string fullPath = ImportDirectoryPath + excelFileName;
        if (!File.Exists(fullPath))
        {
            throw new Exception(
                $"Unable to find Excel workbook in Import directory: \"{excelFileName}\""
            );
        }

        // Import the file as an XSSFWorkbook
        fileStream = new FileStream(fullPath, FileMode.Open);
        var workbook = new XSSFWorkbook(fileStream);

        // Verify that each of the worksheets are named/formatted correctly
        for (int i = 0; i < workbook.NumberOfSheets; ++i)
        {
            ValidateWorksheet((XSSFSheet)workbook.GetSheetAt(i));
        }
    }

    /// <summary>
    /// Validates the provided Excel worksheet. An exception will be thrown if the worksheet is
    /// named incorrectly, doesn't have the correct set of tables, or doesn't have the correct set of columns.
    /// </summary>
    /// <param name="worksheet">Excel worksheet to validate</param>
    public void ValidateWorksheet(XSSFSheet worksheet)
    {
        // Validate that the worksheet is named correctly
        if (
            !DateTime.TryParseExact(
                worksheet.SheetName,
                "yyyy-MM",
                null,
                DateTimeStyles.None,
                out DateTime month
            )
        )
        {
            throw new Exception(
                $"Worksheet name \"{worksheet.SheetName}\" is not a valid date in yyyy-MM format"
            );
        }

        // Verify that all the required tables appear as expected in the worksheet
        List<XSSFTable> worksheetTables = worksheet.GetTables();
        foreach (ExcelTableAttribute expectedTable in ExcelSchema.Keys)
        {
            var actualTableName = GetActualTableName(expectedTable.TableName, month);
            XSSFTable table =
                worksheetTables.FirstOrDefault(
                    worksheetTable => worksheetTable.Name == actualTableName
                )
                ?? throw new Exception(
                    $"Worksheet \"{worksheet.SheetName}\" does not contain table \"{actualTableName}\""
                );

            List<XSSFTableColumn> columns = table.GetColumns();
            foreach (ExcelColumnAttribute expectedColumn in ExcelSchema[expectedTable])
            {
                XSSFTableColumn column =
                    columns.FirstOrDefault(column => column.Name == expectedColumn.ColumnName)
                    ?? throw new Exception(
                        $"Worksheet \"{worksheet.SheetName}\" table \"{actualTableName}\" does not contain column \"{expectedColumn.ColumnName}\""
                    );
            }
        }

        // Verify that the worksheet doesn't contain any extra tables
        if (worksheetTables.Count != ExcelSchema.Keys.Count)
        {
            throw new Exception($"Worksheet \"{worksheet.SheetName}\" contains extra Excel tables");
        }
    }

    /// <summary>
    /// Gets the actual table name as it should appear on an Excel sheet
    /// </summary>
    /// <param name="expectedTableName">Expected table name</param>
    /// <param name="month">DateTime representing the month of the current worksheet</param>
    /// <returns>The actual table name that should appear in the worksheet</returns>
    private static string GetActualTableName(string expectedTableName, DateTime month) =>
        expectedTableName + "." + month.ToString("yyyy.MM");

    /// <summary>
    /// Reflects over the Entities assembly to determine what Excel tables and columns
    /// are required.
    /// </summary>
    /// <returns>A Dictionary mapping the required table names to their list of required columns</returns>
    private static Dictionary<ExcelTableAttribute, List<ExcelColumnAttribute>> GetExcelSchema()
    {
        var excelSchema = new Dictionary<ExcelTableAttribute, List<ExcelColumnAttribute>>();

        // Private method that takes in a type and returns an IEnumerable of the ExcelColumnAttributes on the class
        List<ExcelColumnAttribute> GetColumnsFromType(Type type) =>
            type.GetProperties()
                .SelectMany(
                    property =>
                        Attribute.GetCustomAttributes(property, typeof(ExcelColumnAttribute))
                )
                .Where(attribute => attribute is ExcelColumnAttribute)
                .Select(attribute => (ExcelColumnAttribute)attribute)
                .ToList();

        Assembly entityAssembly =
            Assembly.GetAssembly(typeof(ExcelTableAttribute))
            ?? throw new Exception("Unable to access Entities assembly");
        foreach (Type type in entityAssembly.GetTypes())
        {
            foreach (
                ExcelTableAttribute tableAttribute in Attribute.GetCustomAttributes(
                    type,
                    typeof(ExcelTableAttribute)
                )
            )
            {
                if (excelSchema.ContainsKey(tableAttribute))
                {
                    excelSchema[tableAttribute].Concat(GetColumnsFromType(type));
                }
                else
                {
                    excelSchema.Add(tableAttribute, GetColumnsFromType(type));
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
