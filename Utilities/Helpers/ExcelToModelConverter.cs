using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Helpers;

/// <summary>
/// Class that converts an Excel workbook into the domain models that the data represents. 
/// Assumes that the Excel workbook has already been passed through the ExcelWorkbookValidator.
/// </summary>
public class ExcelToModelConverter(XSSFWorkbook excelWorkbook) : ExcelHelperBase(excelWorkbook)
{
    #region Fields

    private readonly Dictionary<string, ICreateAccountRequest> accountRequests = [];
    private readonly Dictionary<string, ICreateEmployerRequest> employerRequests = [];
    private readonly List<ICreateMonthRequest> monthRequests = [];
    private readonly Dictionary<string, ICreateBudgetRequest> budgetRequests = [];
    private readonly Dictionary<string, ICreateAccountMappingRequest> accountMappingRequests = [];
    private readonly List<ICreateTransactionRequest> transactionRequests = [];
    private readonly List<ICreateIncomeRequest> incomeRequests = [];
    private readonly List<ICreateEmployerIncomeRateRequest> employerIncomeRateRequests = [];
    private readonly List<ICreateAccountBalanceRequest> accountBalanceRequests = [];

    #endregion

    #region Methods

    /// <summary>
    /// Coverts data in the Excel Workbook into the domain models that the data represents
    /// </summary>
    /// <param name="accounts">List of all Accounts converted from the Workbook</param>
    /// <param name="employers">List of all Employers converted from the Workbook</param>
    /// <param name="months">List of all Months converted from the Workbook</param>
    public void Convert(
        out IEnumerable<ICreateAccountRequest> accounts,
        out IEnumerable<ICreateEmployerRequest> employers,
        out IEnumerable<ICreateMonthRequest> months)
    {
        ConvertAccounts();
        ConvertEmployers();
        ConvertMonths();
        if (Exceptions.Count > 0)
        {
            ThrowExceptions();
        }
        accounts = accountRequests.Select(pair => pair.Value);
        employers = employerRequests.Select(pair => pair.Value);
        months = monthRequests;
    }

    /// <summary>
    /// Converts the data in the Account Excel table into Account models
    /// </summary>
    private void ConvertAccounts()
    {
        // Get the Excel column names
        string accountNameColumn = GetExcelColumnNameFromProperty(typeof(IAccount), nameof(IAccount.Name));
        string accountTypeColumn = GetExcelColumnNameFromProperty(typeof(IAccount), nameof(IAccount.Type));

        // Get the setup data worksheet and accounts table
        XSSFSheet setupWorksheet = GetSetupSheet();
        XSSFTable accountsTable = setupWorksheet.GetTables()
            .First(table => table.Name == GetExcelTableName(typeof(IAccount)));

        for (int i = 0; i < accountsTable.RowCount - 1; ++i)
        {
            try
            {
                string accountName = GetTableCell(accountsTable, accountNameColumn, i).StringCellValue;
                accountRequests.Add(accountName, new CreateAccountRequest
                {
                    Name = accountName,
                    Type = Enum.Parse<AccountType>(GetTableCell(accountsTable, accountTypeColumn, i).StringCellValue.Replace(" ", ""))
                });
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
        }
    }

    /// <summary>
    /// Converts the data in the Employers Excel table into Employer models
    /// </summary>
    private void ConvertEmployers()
    {
        // Get the Excel column names
        string employerNameColumn = GetExcelColumnNameFromProperty(typeof(IEmployer), nameof(IEmployer.Name));

        // Get the setup data worksheet and employers table
        XSSFSheet setupWorksheet = GetSetupSheet();
        XSSFTable employersTable = setupWorksheet.GetTables()
            .First(table => table.Name == GetExcelTableName(typeof(IEmployer)));

        for (int i = 0; i < employersTable.RowCount - 1; ++i)
        {
            try
            {
                var employerName = GetTableCell(employersTable, employerNameColumn, i).StringCellValue;
                employerRequests.Add(employerName, new CreateEmployerRequest
                {
                    Name = employerName
                });
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
        }
    }

    /// <summary>
    /// Converts the data in each Monthly Excel sheet into Month models
    /// </summary>
    private void ConvertMonths()
    {
        foreach ((XSSFSheet monthlySheet, DateTime month) in GetMonthlySheets())
        {
            try
            {
                ConvertBudgets(monthlySheet, month);
                ConvertAccountMappings(monthlySheet, month);
                ConvertTransactionsAndApplications(monthlySheet, month);
                ConvertIncomes(monthlySheet, month);
                ConvertEmployerIncomeRates(monthlySheet, month);
                ConvertAccountBalances(monthlySheet, month);
                monthRequests.Add(new CreateMonthRequest
                {
                    Year = month.Year,
                    MonthNumber = month.Month,
                    Budgets = budgetRequests.Select(pair => pair.Value).ToList(),
                    AccountMappings = accountMappingRequests.Select(pair => pair.Value).ToList(),
                    Transactions = transactionRequests,
                    Incomes = incomeRequests,
                    EmployerIncomeRates = employerIncomeRateRequests,
                    AccountBalances = accountBalanceRequests
                });
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }

        }
    }

    /// <summary>
    /// Converts the data in the Budgets Excel table into Budget models
    /// </summary>
    /// <param name="monthlySheet">Excel sheet containing monthly data</param>
    /// <param name="month">DateTime representing the first date of the month for this sheet</param>
    private void ConvertBudgets(XSSFSheet monthlySheet, DateTime month)
    {
        // Get the Excel column names
        string budgetNameColumn = GetExcelColumnNameFromProperty(typeof(IBudget), nameof(IBudget.Name));
        string budgetTypeColumn = GetExcelColumnNameFromProperty(typeof(IBudget), nameof(IBudget.Type));
        string budgetAmountColumn = GetExcelColumnNameFromProperty(typeof(IBudget), nameof(IBudget.Amount));
        string budgetRolloverAmountColumn = GetExcelColumnNameFromProperty(typeof(IBudget), nameof(IBudget.RolloverAmount));
        string budgetOverrideAmountColumn = GetExcelColumnNameFromProperty(typeof(IBudget), nameof(IBudget.IsRolloverAmountOverridden));

        // Get the Budgets table from the monthly sheet
        XSSFTable budgetsTable = monthlySheet.GetTables()
            .First(table => table.Name == GetExcelTableName(typeof(IBudget), month));

        for (int i = 0; i < budgetsTable.RowCount - 1; ++i)
        {
            try
            {
                var budgetName = GetTableCell(budgetsTable, budgetNameColumn, i).StringCellValue;
                budgetRequests.Add(budgetName, new CreateBudgetRequest
                {
                    Name = budgetName,
                    Type = Enum.Parse<BudgetType>(GetTableCell(budgetsTable, budgetTypeColumn, i).StringCellValue.Replace(" ", "")),
                    Amount = (decimal)GetTableCell(budgetsTable, budgetAmountColumn, i).NumericCellValue,
                    RolloverAmount = (decimal?)GetTableCellOrNull(budgetsTable, budgetRolloverAmountColumn, i)?.NumericCellValue,
                    IsRolloverAmountOverridden = GetTableCellOrNull(budgetsTable, budgetOverrideAmountColumn, i)?.BooleanCellValue
                });
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
        }
    }

    /// <summary>
    /// Converts the data in the Account Mappings Excel table into Account Mapping models
    /// </summary>
    /// <param name="monthlySheet">Excel sheet containing monthly data</param>
    /// <param name="month">DateTime representing the first date of the month for this sheet</param>
    private void ConvertAccountMappings(XSSFSheet monthlySheet, DateTime month)
    {
        // Get the Excel column names
        string mappingAccountNameColumn = GetExcelColumnNameFromProperty(typeof(IAccountMapping), nameof(IAccountMapping.AccountId));
        string mappingStartingBalanceColumn = GetExcelColumnNameFromProperty(typeof(IAccountMapping), nameof(IAccountMapping.StartingBalance));
        string mappingBudgetNameColumn = GetExcelColumnNameFromProperty(typeof(IAccountMapping), nameof(IAccountMapping.BudgetId));
        string mappingBudgetTypeColumn = GetExcelColumnNameFromProperty(typeof(IAccountMapping), nameof(IAccountMapping.BudgetType));
        string mappingIsDefaultColumn = GetExcelColumnNameFromProperty(typeof(IAccountMapping), nameof(IAccountMapping.IsDefault));

        // Get the Account Mappings table from the monthly sheet
        XSSFTable accountMappingsTable = monthlySheet.GetTables()
            .First(table => table.Name == GetExcelTableName(typeof(IAccountMapping), month));

        for (int i = 0; i < accountMappingsTable.RowCount - 1; ++i)
        {
            try
            {
                string accountName = GetTableCell(accountMappingsTable, mappingAccountNameColumn, i).StringCellValue;
                string? budgetName = GetTableCellOrNull(accountMappingsTable, mappingBudgetNameColumn, i)?.StringCellValue;
                string? budgetType = GetTableCellOrNull(accountMappingsTable, mappingBudgetTypeColumn, i)?.StringCellValue;
                accountMappingRequests.Add(accountName, new CreateAccountMappingRequest
                {
                    AccountId = accountRequests[accountName].Id,
                    StartingBalance = (decimal)GetTableCell(accountMappingsTable, mappingStartingBalanceColumn, i).NumericCellValue,
                    BudgetId = budgetName == null ? null : budgetRequests[budgetName].Id,
                    BudgetType = budgetType == null ? null : Enum.Parse<BudgetType>(budgetType.Replace(" ", "")),
                    IsDefault = GetTableCell(accountMappingsTable, mappingIsDefaultColumn, i).BooleanCellValue
                });
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
        }
    }

    /// <summary>
    /// Converts the data in the Transactions Excel table into Transaction models
    /// </summary>
    /// <param name="monthlySheet">Excel sheet containing monthly data</param>
    /// <param name="month">DateTime representing the first date of the month for this sheet</param>
    private void ConvertTransactionsAndApplications(XSSFSheet monthlySheet, DateTime month)
    {
        // Get the Excel column names
        string transactionDateColumn = GetExcelColumnNameFromProperty(typeof(ITransaction), nameof(ITransaction.Date));
        string transactionLocationColumn = GetExcelColumnNameFromProperty(typeof(ITransaction), nameof(ITransaction.Location));
        string transactionTypeColumn = GetExcelColumnNameFromProperty(typeof(ITransaction), nameof(ITransaction.Type));
        string transactionAmountColumn = GetExcelColumnNameFromProperty(typeof(ITransaction), nameof(ITransaction.Amount));
        string transactionCreditCardAccountColumn = GetExcelColumnNameFromProperty(typeof(ITransaction), nameof(ITransaction.CreditCardAccountId));
        string transactionPaidOffDateColumn = GetExcelColumnNameFromProperty(typeof(ITransaction), nameof(ITransaction.PaidOffDate));

        // Get the Transaction table from the monthly sheet
        XSSFTable transactionTable = monthlySheet.GetTables()
            .First(table => table.Name == GetExcelTableName(typeof(ITransaction), month));

        foreach (int rowIndex in GetTransactionRowIndexes(transactionTable))
        {
            try
            {
                string? creditCardAccountName = GetTableCellOrNull(transactionTable, transactionCreditCardAccountColumn, rowIndex)?.StringCellValue;
                int? paidOffDateAsNumeric = (int?)GetTableCellOrNull(transactionTable, transactionPaidOffDateColumn, rowIndex)?.NumericCellValue;
                transactionRequests.Add(new CreateTransactionRequest
                {
                    Date = DateTime.FromOADate(GetTableCell(transactionTable, transactionDateColumn, rowIndex).NumericCellValue),
                    Location = GetTableCell(transactionTable, transactionLocationColumn, rowIndex).StringCellValue,
                    Type = Enum.Parse<TransactionType>(GetTableCell(transactionTable, transactionTypeColumn, rowIndex).StringCellValue),
                    Amount = (decimal)GetTableCell(transactionTable, transactionAmountColumn, rowIndex).NumericCellValue,
                    CreditCardAccountId = creditCardAccountName == null ? null : accountRequests[creditCardAccountName].Id,
                    PaidOffDate = paidOffDateAsNumeric == null ? null : DateTime.FromOADate(paidOffDateAsNumeric.Value),
                    CreateTransactionApplicationRequests = ConvertTransactionApplications(monthlySheet, month, rowIndex).ToList()
                });
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
        }
    }

    /// <summary>
    /// Converts the data in the Transaction Excel table into Transaction Application models
    /// </summary>
    /// <param name="monthlySheet">Excel sheet containing monthly data</param>
    /// <param name="month">DateTime representing the first date of the month for this sheet</param>
    /// <param name="transactionIndex">Index of the current transaction in the Transaction table</param>
    /// <returns>An IEnumerable of Transaction Application models for the current transaction</returns>
    private List<ICreateTransactionApplicationRequest> ConvertTransactionApplications(XSSFSheet monthlySheet, DateTime month, int transactionIndex)
    {
        var applicationRequests = new List<ICreateTransactionApplicationRequest>();

        // Get the Excel column names
        string applicationTypeColumn = GetExcelColumnNameFromProperty(typeof(ITransactionApplication), nameof(ITransactionApplication.Type));
        string applicationDescriptionColumn = GetExcelColumnNameFromProperty(typeof(ITransactionApplication), nameof(ITransactionApplication.Description));
        string applicationAmountColumn = GetExcelColumnNameFromProperty(typeof(ITransactionApplication), nameof(ITransactionApplication.Amount));
        string applicationBudgetColumn = GetExcelColumnNameFromProperty(typeof(ITransactionApplication), nameof(ITransactionApplication.BudgetId));
        string applicationOverrideAccountColumn = GetExcelColumnNameFromProperty(typeof(ITransactionApplication), nameof(ITransactionApplication.OverrideAccountMappingId));

        // Get the Transaction table from the monthly sheet
        XSSFTable transactionTable = monthlySheet.GetTables()
            .First(table => table.Name == GetExcelTableName(typeof(ITransaction), month));

        foreach (int rowIndex in GetTransactionApplicationRowIndexes(transactionTable, transactionIndex))
        {
            try
            {
                string? accountOverrideMappingName = GetTableCellOrNull(transactionTable, applicationOverrideAccountColumn, rowIndex)?.StringCellValue;
                applicationRequests.Add(new CreateTransactionApplicationRequest
                {
                    Type = Enum.Parse<ApplicationType>(GetTableCell(transactionTable, applicationTypeColumn, rowIndex).StringCellValue),
                    Description = GetTableCell(transactionTable, applicationDescriptionColumn, rowIndex).StringCellValue,
                    Amount = (decimal)GetTableCell(transactionTable, applicationAmountColumn, rowIndex).NumericCellValue,
                    BudgetId = budgetRequests[GetTableCell(transactionTable, applicationBudgetColumn, rowIndex).StringCellValue].Id,
                    OverrideAccountMappingId = accountOverrideMappingName == null ? null : accountMappingRequests[accountOverrideMappingName].Id
                });
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
        }
        return applicationRequests;
    }

    /// <summary>
    /// Returns a list of row indexes in the Transactions table that contain Transaction data 
    /// </summary>
    /// <param name="transactionTable">Excel table containing Transaction data</param>
    /// <returns>An IEnumerable of integers representing rows in the Transactions table that contain Transactions</returns>
    private static IEnumerable<int> GetTransactionRowIndexes(XSSFTable transactionTable) =>
        // Any row that has a Transaction must have the Date column populated
        from i in Enumerable.Range(0, transactionTable.RowCount - 1)
        where GetTableCellOrNull(transactionTable, GetExcelColumnNameFromProperty(typeof(ITransaction), nameof(ITransaction.Date)), i) != null
        select i;

    /// <summary>
    /// Given a row in the Transaction table, returns a list of row indexes that indicate which rows contain
    /// the related transaction applications
    /// </summary>
    /// <param name="transactionTableIndex">Index of a particular row in the Transactions table</param>
    /// <returns>An IEnumerable of integers representing rows in the Transactions table that contain 
    /// Transaction Applications related to the provided Transaction row</returns>
    private static IEnumerable<int> GetTransactionApplicationRowIndexes(XSSFTable transactionTable, int transactionTableIndex)
    {
        // If the transaction row has a Description, this transaction only has a single application and it's stored
        // in the same row as the transaction
        if (GetTableCellOrNull(transactionTable,
                GetExcelColumnNameFromProperty(typeof(ITransactionApplication), nameof(ITransactionApplication.Description)),
                transactionTableIndex) != null)
        {
            return Enumerable.Range(transactionTableIndex, 1);
        }

        // Otherwise, the transaction has multiple applications and each application will be stored in a separate row
        int firstApplicationIndex = transactionTableIndex + 1;
        int lastApplicationIndex = firstApplicationIndex;
        while (lastApplicationIndex < transactionTable.RowCount - 2 &&
            GetTableCellOrNull(transactionTable,
                GetExcelColumnNameFromProperty(typeof(ITransaction), nameof(ITransaction.Date)),
                lastApplicationIndex + 1) == null)
        {
            lastApplicationIndex++;
        }
        return Enumerable.Range(firstApplicationIndex, lastApplicationIndex - firstApplicationIndex + 1);
    }

    /// <summary>
    /// Converts the data in the Income Excel table into Income models
    /// </summary>
    /// <param name="monthlySheet">Excel sheet containing monthly data</param>
    /// <param name="month">DateTime representing the first date of the month for this sheet</param>
    private void ConvertIncomes(XSSFSheet monthlySheet, DateTime month)
    {
        // Get the Excel column names
        string incomeEmployerColumn = GetExcelColumnNameFromProperty(typeof(IIncome), nameof(IIncome.EmployerId));
        string incomeDateColumn = GetExcelColumnNameFromProperty(typeof(IIncome), nameof(IIncome.Date));
        string incomeDescriptionColumn = GetExcelColumnNameFromProperty(typeof(IIncome), nameof(IIncome.Description));
        string incomeAmountColumn = GetExcelColumnNameFromProperty(typeof(IIncome), nameof(IIncome.Amount));
        string incomeAccountColumn = GetExcelColumnNameFromProperty(typeof(IIncome), nameof(IIncome.OverrideAccountMappingId));

        // Get the Income table from the monthly sheet
        XSSFTable incomeTable = monthlySheet.GetTables()
            .First(table => table.Name == GetExcelTableName(typeof(IIncome), month));

        for (int i = 0; i < incomeTable.RowCount - 1; ++i)
        {
            try
            {
                string? incomeEmployerName = GetTableCellOrNull(incomeTable, incomeEmployerColumn, i)?.StringCellValue;
                string? incomeAccountOverride = GetTableCellOrNull(incomeTable, incomeAccountColumn, i)?.StringCellValue;
                incomeRequests.Add(new CreateIncomeRequest
                {
                    EmployerId = incomeEmployerName == null ? null : employerRequests[incomeEmployerName].Id,
                    Date = DateTime.FromOADate(GetTableCell(incomeTable, incomeDateColumn, i).NumericCellValue),
                    Amount = (decimal)GetTableCell(incomeTable, incomeAmountColumn, i).NumericCellValue,
                    Description = GetTableCell(incomeTable, incomeDescriptionColumn, i).StringCellValue,
                    OverrideAccountMappingId = incomeAccountOverride == null ? null : accountRequests[incomeAccountOverride].Id,
                    CreateEmployerIncomeInformationRequest = ConvertEmployerIncomeInformation(incomeTable, i)
                });
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
        }
    }

    /// <summary>
    /// Converts the data in the Employer Income Rate table into Employer Income Rate models
    /// </summary>
    /// <param name="monthlySheet">Excel sheet containing monthly data</param>
    /// <param name="month">DateTime representing the first date of the month for this sheet</param>
    private void ConvertEmployerIncomeRates(XSSFSheet monthlySheet, DateTime month)
    {
        // Get the Excel column names
        string employerColumn = GetExcelColumnNameFromProperty(typeof(IEmployerIncomeRate), nameof(IEmployerIncomeRate.EmployerId));

        // Get the Employer Income Rate table from the monthly sheet
        XSSFTable employerIncomeRateTable = monthlySheet.GetTables()
            .First(table => table.Name == GetExcelTableName(typeof(IEmployerIncomeRate), month));

        for (int i = 0; i < employerIncomeRateTable.RowCount - 1; ++i)
        {
            try
            {
                employerIncomeRateRequests.Add(new CreateEmployerIncomeRateRequest
                {
                    EmployerId = employerRequests[GetTableCell(employerIncomeRateTable, employerColumn, i).StringCellValue].Id,
                    CreateEmployerIncomeInformationRequest = ConvertEmployerIncomeInformation(employerIncomeRateTable, i) ?? throw new InvalidOperationException()
                });
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
        }
    }

    /// <summary>
    /// Converts the data in the provided table into Employer Income Information models
    /// </summary>
    /// <param name="table">Table containing Employer Income Information data</param>
    /// <param name="rowIndex">Index of the current row in the provided table</param>
    /// <returns>The Employer Income Information model for the current row in the provided table</returns>
    private CreateEmployerIncomeInformationRequest? ConvertEmployerIncomeInformation(XSSFTable table, int rowIndex)
    {
        // Get the Excel column names
        string employerSalaryIncomeColumn = GetExcelColumnNameFromProperty(typeof(IEmployerIncomeInformation), nameof(IEmployerIncomeInformation.SalaryIncome));
        string employerAdditionalTaxableIncomeColumn = GetExcelColumnNameFromProperty(typeof(IEmployerIncomeInformation), nameof(IEmployerIncomeInformation.AdditionalTaxableIncome));
        string employerRetirementColumn = GetExcelColumnNameFromProperty(typeof(IEmployerIncomeInformation), nameof(IEmployerIncomeInformation.RetirementContributionAmount));
        string employerPensionColumn = GetExcelColumnNameFromProperty(typeof(IEmployerIncomeInformation), nameof(IEmployerIncomeInformation.PensionContributionAmount));
        string employerPreTaxDeductionColumn = GetExcelColumnNameFromProperty(typeof(IEmployerIncomeInformation), nameof(IEmployerIncomeInformation.PreTaxDeductionAmount));
        string employerTaxAmountColumn = GetExcelColumnNameFromProperty(typeof(IEmployerIncomeInformation), nameof(IEmployerIncomeInformation.TaxWithholdingAmount));
        string employerPostTaxDeductionColumn = GetExcelColumnNameFromProperty(typeof(IEmployerIncomeInformation), nameof(IEmployerIncomeInformation.PostTaxDeductionAmount));

        decimal? salaryIncomeAmount = (decimal?)GetTableCellOrNull(table, employerSalaryIncomeColumn, rowIndex)?.NumericCellValue;
        CreateEmployerIncomeInformationRequest? employerIncomeInformation = null;
        try
        {
            if (salaryIncomeAmount != null)
            {
                employerIncomeInformation = new CreateEmployerIncomeInformationRequest
                {
                    SalaryIncome = salaryIncomeAmount.Value,
                    AdditionalTaxableIncome = (decimal)GetTableCell(table, employerAdditionalTaxableIncomeColumn, rowIndex).NumericCellValue,
                    RetirementContributionAmount = (decimal)GetTableCell(table, employerRetirementColumn, rowIndex).NumericCellValue,
                    PensionContributionAmount = (decimal)GetTableCell(table, employerPensionColumn, rowIndex).NumericCellValue,
                    PreTaxDeductionAmount = (decimal)GetTableCell(table, employerPreTaxDeductionColumn, rowIndex).NumericCellValue,
                    TaxWithholdingAmount = (decimal)GetTableCell(table, employerTaxAmountColumn, rowIndex).NumericCellValue,
                    PostTaxDeductionAmount = (decimal)GetTableCell(table, employerPostTaxDeductionColumn, rowIndex).NumericCellValue
                };
            }
        }
        catch (Exception e)
        {
            Exceptions.Add(e);
        }
        return employerIncomeInformation;
    }

    /// <summary>
    /// Converts the data in the Account Balance table into Account Balance models
    /// </summary>
    /// <param name="monthlySheet">Excel sheet containing monthly data</param>
    /// <param name="month">DateTime representing the first date of the month for this sheet</param>
    private void ConvertAccountBalances(XSSFSheet monthlySheet, DateTime month)
    {
        // Get the Excel column names
        string accountBalanceAccountColumn = GetExcelColumnNameFromProperty(typeof(IAccountBalance), nameof(IAccountBalance.AccountId));
        string accountBalanceDateColumn = GetExcelColumnNameFromProperty(typeof(IAccountBalance), nameof(IAccountBalance.Date));
        string accountBalanceAmountColumn = GetExcelColumnNameFromProperty(typeof(IAccountBalance), nameof(IAccountBalance.Amount));

        // Get the Account Balance table from the monthly sheet
        XSSFTable accountBalanceTable = monthlySheet.GetTables()
            .First(table => table.Name == GetExcelTableName(typeof(IAccountBalance), month));

        for (int i = 0; i < accountBalanceTable.RowCount - 1; ++i)
        {
            try
            {
                accountBalanceRequests.Add(new CreateAccountBalanceRequest
                {
                    AccountId = accountRequests[GetTableCell(accountBalanceTable, accountBalanceAccountColumn, i).StringCellValue].Id,
                    Date = DateTime.FromOADate(GetTableCell(accountBalanceTable, accountBalanceDateColumn, i).NumericCellValue),
                    Amount = (decimal)GetTableCell(accountBalanceTable, accountBalanceAmountColumn, i).NumericCellValue
                });
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
        }
    }

    #endregion
}