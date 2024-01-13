using Configuration;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;
using Utilities.Excel;
using Utilities.Excel.ModelConverters;

namespace Utilities;

/// <summary>
/// Class that manages importing data from an Excel spreadsheet and uploads it to the database.
/// </summary>
public class DataImportUtility(string excelFileName) : IDisposable
{
    #region Fields

    /// <summary>
    /// File stream to use to import the Excel file
    /// </summary>
    private FileStream? fileStream;

    #endregion

    #region Methods

    /// <summary>
    /// Execute the data import action.
    /// Imports data from the provided Excel spreadsheet and uploads it to the SQL database through
    /// the REST API. Validates that each sheet in the Excel spreadsheet is formatted correctly.
    /// </summary>
    public void Execute()
    {
        // Import the workbook
        XSSFWorkbook excelWorkbook = ImportWorkbook();

        // Validate the workbook's structure
        ValidateWorkbook(excelWorkbook);

        // Build models using the data in the workbook
        ConvertToModels(excelWorkbook,
            out IEnumerable<ICreateAccountRequest> accountRequests,
            out IEnumerable<ICreateEmployerRequest> employerRequests,
            out IEnumerable<ICreateMonthRequest> monthRequests);
    }

    /// <summary>
    /// Imports the Excel workbook as an XSSFWorkbook. Throws an error if the ExcelFileName does not exist in the Import 
    /// directory or if the file located at the path is not a valid Excel workbook.
    /// </summary>
    /// <returns>An XSSFWorkbook representing the Excel workbook found at ExcelFileName</returns>
    private XSSFWorkbook ImportWorkbook()
    {
        // Verify that the file exists
        string fullPath = AppConfiguration.ImportDirectory + "\\" + excelFileName;
        if (!File.Exists(fullPath))
        {
            Console.WriteLine(fullPath);
            throw new Exception($"Unable to find Excel workbook in Import directory: \"{excelFileName}\"");
        }

        // Import the file as an XSSFWorkbook
        fileStream = new FileStream(fullPath, FileMode.Open);
        return new XSSFWorkbook(fileStream);
    }

    /// <summary>
    /// Given an Excel workbook, validates that the workbook is structured correctly for importing
    /// </summary>
    /// <param name="excelWorkbook">Excel workbook with data to validate</param>
    private static void ValidateWorkbook(XSSFWorkbook excelWorkbook)
    {
        var workbookValidator = new ExcelWorkbookValidator(excelWorkbook);
        workbookValidator.Validate(out IEnumerable<Exception> validationExceptions);
        ThrowExceptions(validationExceptions);
    }

    /// <summary>
    /// Given an Excel workbook, converts all the data in the workbook into models
    /// </summary>
    /// <param name="workbook">Excel workbook with data to convert</param>
    /// <param name="accountRequests">List of converted account requests</param>
    /// <param name="employerRequests">List of converted employer requests</param>
    /// <param name="monthRequests">List of converted month requests</param>
    private static void ConvertToModels(
        XSSFWorkbook workbook,
        out IEnumerable<ICreateAccountRequest> accountRequests,
        out IEnumerable<ICreateEmployerRequest> employerRequests,
        out IEnumerable<ICreateMonthRequest> monthRequests)
    {
        List<Exception> exceptionList = [];

        // Convert the models found on the setup worksheet
        XSSFSheet setupWorksheet = ExcelDataHelper.GetSetupSheet(workbook);
        var accountConverter = new AccountConverter(setupWorksheet);
        accountRequests = accountConverter.ConvertExcelToModels(null, out IEnumerable<Exception> accountExceptions);
        exceptionList.AddRange(accountExceptions);
        var employerConverter = new EmployerConverter(setupWorksheet);
        employerRequests = employerConverter.ConvertExcelToModels(null, out IEnumerable<Exception> employerExceptions);
        exceptionList.AddRange(employerExceptions);

        // Convert the models found on the monthly worksheets
        var monthConverter = new MonthConverter(accountRequests, employerRequests);
        monthRequests = ExcelDataHelper.GetMonthlySheets(workbook).Select(pair =>
            {
                ICreateMonthRequest monthRequest = monthConverter.ConvertExcelToModels(
                    pair.sheet,
                    out IEnumerable<Exception> monthExceptions);
                exceptionList.AddRange(monthExceptions);
                return monthRequest;
            });

        // Throw an exceptions that were encountered
        ThrowExceptions(exceptionList);
    }

    /// <summary>
    /// Throws a single exception that outlines all the provided exceptions
    /// </summary>
    private static void ThrowExceptions(IEnumerable<Exception> exceptions)
    {
        if (exceptions.Any())
        {
            List<string> outputStringList = exceptions.Select(exception => exception.Message).ToList();
            outputStringList.Insert(0, "The following errors were encountered while validating the workbook:");
            throw new Exception(string.Join("\n", outputStringList));
        }
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
