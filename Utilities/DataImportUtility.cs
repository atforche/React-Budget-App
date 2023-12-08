using Configuration;
using Models;
using NPOI.XSSF.UserModel;
using Utilities.Helpers;

namespace BudgetUtilities;

/// <summary>
/// Class that manages importing data from an Excel spreadsheet and uploads it
/// to the database.
/// </summary>
/// <remarks>
/// Constructs a new instance of this class
/// </remarks>
public class DataImportUtility(string excelFileName) : IDisposable
{
    #region Fields

    private FileStream? fileStream;

    #endregion

    #region Properties

    /// <summary>
    /// Name of the Excel file to import from the Import directory
    /// </summary>
    private string ExcelFileName { get; init; } = excelFileName;

    #endregion

    #region Methods

    /// <summary>
    /// Execute the data import action.
    /// Imports data from the provided Excel spreadsheet and uploads it to the SQL database through
    /// the REST API. Validates that each sheet in the Excel spreadsheet is formatted correctly.
    /// </summary>
    public void Execute()
    {
        XSSFWorkbook excelWorkbook = ImportWorkbook();

        var workbookValidator = new ExcelWorkbookValidator(excelWorkbook);
        workbookValidator.Validate();

        var excelConverter = new ExcelToModelConverter(excelWorkbook);
        excelConverter.Convert(out IEnumerable<ICreateAccountRequest> accounts,
            out IEnumerable<ICreateEmployerRequest> employers,
            out IEnumerable<ICreateMonthRequest> months);
    }

    /// <summary>
    /// Imports the Excel workbook as an XSSFWorkbook. Throws an error if the ExcelFileName does not exist in the Import 
    /// directory or if the file located at the path is not a valid Excel workbook.
    /// </summary>
    /// <returns>An XSSFWorkbook representing the Excel workbook found at ExcelFileName</returns>
    public XSSFWorkbook ImportWorkbook()
    {
        // Verify that the file exists
        string fullPath = AppConfiguration.ImportDirectory + "\\" + ExcelFileName;
        if (!File.Exists(fullPath))
        {
            Console.WriteLine(fullPath);
            throw new Exception($"Unable to find Excel workbook in Import directory: \"{ExcelFileName}\"");
        }

        // Import the file as an XSSFWorkbook
        fileStream = new FileStream(fullPath, FileMode.Open);
        return new XSSFWorkbook(fileStream);
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
