using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Transaction models to and from Excel format
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
/// <param name="accountRequests">List of converted account models</param>
/// <param name="budgetRequests">List of converted budget models</param>
public class TransactionConverter(
    XSSFSheet worksheet,
    IEnumerable<ICreateAccountRequest> accountRequests,
    IEnumerable<ICreateBudgetRequest> budgetRequests)
    : ExcelModelConverter<ITransaction, ICreateTransactionRequest>(worksheet)
{
    #region Fields

    /// <summary>
    /// Dictionary mapping an account name to its associated request
    /// </summary>
    private readonly Dictionary<string, ICreateAccountRequest> accountNameToRequest =
        accountRequests.ToDictionary(account => account.Name);

    /// <summary>
    /// Child model converter for Transaction Applications
    /// </summary>
    private readonly TransactionApplicationConverter transactionApplicationConverter =
        new(worksheet, typeof(ITransaction), budgetRequests, accountRequests);

    #endregion

    #region Properties

    /// <inheritdoc/>
    protected override Dictionary<string, Func<XSSFCell, object?>> ColumnConverters =>
        new()
        {
            [nameof(ITransaction.Date)] =
                DefaultColumnConverters.DefaultDateTimePropertyConverter,
            [nameof(ITransaction.Location)] =
                DefaultColumnConverters.DefaultStringPropertyConverter,
            [nameof(ITransaction.Type)] =
                DefaultColumnConverters.DefaultEnumPropertyConverter<TransactionType>(),
            [nameof(ITransaction.Amount)] =
                DefaultColumnConverters.DefaultDecimalPropertyConverter,
            [nameof(ITransaction.CreditCardAccountId)] = DefaultColumnConverters.AsNullableConverter(
                cell => accountNameToRequest[DefaultColumnConverters.DefaultStringPropertyConverter(cell)].Id),
            [nameof(ITransaction.PaidOffDate)] = DefaultColumnConverters.AsNullableConverter(
                DefaultColumnConverters.DefaultDateTimePropertyConverter)
        };

    /// <inheritdoc/>
    protected override Dictionary<string, IExcelModelConverter> ChildModelConverters =>
        new()
        {
            [nameof(ITransaction.TransactionApplications)] = transactionApplicationConverter
        };

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override ICreateTransactionRequest GetDefaultCreateRequest() =>
        new CreateTransactionRequest()
        {
            Date = DateTime.Today,
            Location = "",
            Type = TransactionType.Debit,
            Amount = 0.00m,
            CreditCardAccountId = null,
            PaidOffDate = null,
            TransactionApplications = []
        };

    /// <inheritdoc/>
    protected override ExcelRowRange? GetChildModelIndexes(Type childType, XSSFTable table)
    {
        if (childType == typeof(ITransactionApplication))
        {
            return GetChildApplicationIndexes(table, CurrentIndex);
        }
        return null;
    }

    /// <summary>
    /// Determines the range of Excel rows that contain child transaction applications for the transaction
    /// found at the provided row
    /// </summary>
    /// <param name="table">Excel table containing transactions and transaction applications</param>
    /// <param name="parentIndex">Row index of the parent transaction</param>
    /// <returns>An ExcelRowRange representing the rows that contain child transaction applications</returns>
    private ExcelRowRange GetChildApplicationIndexes(XSSFTable table, int parentIndex)
    {
        string transactionDateColumn = ExcelDataHelper.GetColumnNameForProperty(typeof(ITransaction),
            nameof(ITransaction.Date));
        string applicationDescriptionColumn = ExcelDataHelper.GetColumnNameForProperty(typeof(ITransactionApplication),
            nameof(ITransactionApplication.Description));
        Func<XSSFCell, object?> dateConverter =
            DefaultColumnConverters.AsNullableConverter(DefaultColumnConverters.DefaultDateTimePropertyConverter);
        Func<XSSFCell, object?> descriptionConverter =
            DefaultColumnConverters.AsNullableConverter(DefaultColumnConverters.DefaultStringPropertyConverter);

        // Ensure the table has the needed columns to extract the child index
        List<XSSFTableColumn> columns = table.GetColumns();
        if (!columns.Any(column => column.Name == transactionDateColumn) ||
            !columns.Any(column => column.Name == applicationDescriptionColumn))
        {
            throw new Exception($"Table \"{table.Name}\" does not have the needed columns to convert " +
                $"\"{nameof(ITransactionApplication)}s\"");
        }

        // If the transaction's date and the applications description are provided in the same row, this transaction
        // only has a single application and they're both stored in the same row
        if (dateConverter.Invoke(ExcelDataHelper.GetTableCell(table, transactionDateColumn, parentIndex)) != null &&
            descriptionConverter.Invoke(ExcelDataHelper.GetTableCell(table, applicationDescriptionColumn, parentIndex))
                != null)
        {
            return new ExcelRowRange(parentIndex, parentIndex);
        }
        // Otherwise, the transaction's applications are stored in a contiguous set of rows directly following the
        // parent transaction's row
        int minimumIndex = parentIndex + 1;
        int firstNonChildIndex = minimumIndex + 1;
        if (minimumIndex > ExcelDataHelper.GetMaximumRowIndex(table))
        {
            throw new Exception($"Expected child transaction application in table \"{table.Name}\" " +
                $"but found end of table instead");
        }
        while (firstNonChildIndex <= ExcelDataHelper.GetMaximumRowIndex(table) &&
            dateConverter.Invoke(ExcelDataHelper.GetTableCell(table, transactionDateColumn, parentIndex)) == null &&
            descriptionConverter.Invoke(ExcelDataHelper.GetTableCell(table, applicationDescriptionColumn, parentIndex))
                != null)
        {
            firstNonChildIndex++;
        }
        return new ExcelRowRange(minimumIndex, firstNonChildIndex - 1);
    }

    #endregion
}