using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Transaction Application models to and from Excel format.
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
/// <param name="parentType">If this converter deals with child models, the parent type of this model converter</param>
/// <param name="budgetRequests">List of converted budget models</param>
/// <param name="accountRequests">List of converted account models</param>
public class TransactionApplicationConverter(
    XSSFSheet worksheet,
    Type? parentType,
    IEnumerable<ICreateBudgetRequest> budgetRequests,
    IEnumerable<ICreateAccountRequest> accountRequests)
    : ExcelModelConverter<ITransactionApplication, ICreateTransactionApplicationRequest>(worksheet, parentType)
{
    #region Fields

    /// <summary>
    /// Dictionary mapping a budget name to its associated request
    /// </summary>
    private readonly Dictionary<string, ICreateBudgetRequest> budgetNameToRequest =
        budgetRequests.ToDictionary(budget => budget.Name);

    /// <summary>
    /// Dictionary mapping an account name to its associated request
    /// </summary>
    private readonly Dictionary<string, ICreateAccountRequest> accountNameToRequest =
        accountRequests.ToDictionary(account => account.Name);

    #endregion

    #region Properties

    /// <inheritdoc/>
    protected override Dictionary<string, Func<XSSFCell, object?>> ColumnConverters =>
        new()
        {
            [nameof(ITransactionApplication.Type)] =
                DefaultColumnConverters.DefaultEnumPropertyConverter<ApplicationType>(),
            [nameof(ITransactionApplication.Description)] =
                DefaultColumnConverters.DefaultStringPropertyConverter,
            [nameof(ITransactionApplication.Amount)] =
                DefaultColumnConverters.DefaultDecimalPropertyConverter,
            [nameof(ITransactionApplication.BudgetId)] =
                cell => budgetNameToRequest[DefaultColumnConverters.DefaultStringPropertyConverter(cell)].Id,
            [nameof(ITransactionApplication.OverrideAccountMappingId)] = DefaultColumnConverters.AsNullableConverter(
                cell => accountNameToRequest[DefaultColumnConverters.DefaultStringPropertyConverter(cell)].Id)
        };

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override ICreateTransactionApplicationRequest GetDefaultCreateRequest() =>
        new CreateTransactionApplicationRequest()
        {
            Type = ApplicationType.Debit,
            Description = "",
            Amount = 0.00m,
            BudgetId = 0L,
            OverrideAccountMappingId = null
        };

    #endregion
}