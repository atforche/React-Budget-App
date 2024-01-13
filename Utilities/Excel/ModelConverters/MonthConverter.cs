using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Month models to and from Excel format.
/// </summary>
/// <remarks>
/// The month converter is unique because a Month model is not associated with a single table. Instead, a month
/// model is represented by an entire worksheet and all the individual models contained within that worksheet
/// </remarks>
/// <param name="accountRequests">List of converted account models</param>
/// <param name="employerRequests">List of converted employer models</param>
public class MonthConverter(
    IEnumerable<ICreateAccountRequest> accountRequests,
    IEnumerable<ICreateEmployerRequest> employerRequests)
{
    /// <summary>
    /// Converts all the monthly data in the provided Excel worksheet into a single CreateMonthRequest
    /// </summary>
    /// <param name="worksheet">Excel worksheet containing monthly model data</param>
    /// <param name="exceptions">Exceptions encountered during the conversion process</param>
    /// <returns>A CreateMonthRequest containing all the monthly data on the provided sheet</returns>
    public CreateMonthRequest ConvertExcelToModels(
        XSSFSheet worksheet,
        out IEnumerable<Exception> exceptions)
    {
        List<Exception> exceptionList = [];
        if (!ExcelDataHelper.IsValidMonthlySheetName(worksheet.SheetName, out DateTime? month))
        {
            throw new Exception($"Provided worksheet \"{worksheet.SheetName}\" does not contain monthly data");
        }

        var budgetConverter = new BudgetConverter(worksheet);
        IEnumerable<ICreateBudgetRequest> budgetRequests =
            budgetConverter.ConvertExcelToModels(null, out IEnumerable<Exception> budgetExceptions);
        exceptionList.AddRange(budgetExceptions);

        var accountMappingConverter = new AccountMappingConverter(worksheet, accountRequests, budgetRequests);
        IEnumerable<ICreateAccountMappingRequest> accountMappingRequests =
            accountMappingConverter.ConvertExcelToModels(null, out IEnumerable<Exception> accountMappingExceptions);
        exceptionList.AddRange(accountMappingExceptions);

        var transactionConverter = new TransactionConverter(worksheet, accountRequests, budgetRequests);
        IEnumerable<ICreateTransactionRequest> transactionRequests =
            transactionConverter.ConvertExcelToModels(null, out IEnumerable<Exception> transactionExceptions);
        exceptionList.AddRange(transactionExceptions);

        var incomeConverter = new IncomeConverter(worksheet, employerRequests, accountRequests);
        IEnumerable<ICreateIncomeRequest> incomeRequests =
            incomeConverter.ConvertExcelToModels(null, out IEnumerable<Exception> incomeExceptions);
        exceptionList.AddRange(incomeExceptions);

        var employerIncomeRateConverter = new EmployerIncomeRateConverter(worksheet, employerRequests);
        IEnumerable<ICreateEmployerIncomeRateRequest> employerIncomeRateRequests =
            employerIncomeRateConverter.ConvertExcelToModels(
                null,
                out IEnumerable<Exception> employerIncomeRateExceptions);
        exceptionList.AddRange(employerIncomeRateExceptions);

        var accountBalanceConverter = new AccountBalanceConverter(worksheet, accountRequests);
        IEnumerable<ICreateAccountBalanceRequest> accountBalanceRequests =
            accountBalanceConverter.ConvertExcelToModels(null, out IEnumerable<Exception> accountBalanceExceptions);
        exceptionList.AddRange(accountBalanceExceptions);

        exceptions = exceptionList;
        return new CreateMonthRequest
        {
            Year = month.Value.Year,
            MonthNumber = month.Value.Month,
            Budgets = budgetRequests.ToList(),
            AccountMappings = accountMappingRequests.ToList(),
            Transactions = transactionRequests.ToList(),
            Incomes = incomeRequests.ToList(),
            EmployerIncomeRates = employerIncomeRateRequests.ToList(),
            AccountBalances = accountBalanceRequests.ToList()
        };
    }
}