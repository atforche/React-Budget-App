using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Account Mapping models to and from Excel format.
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
/// <param name="accountRequests">List of converted account models</param>
/// <param name="budgetRequests">List of converted budget models</param>
public class AccountMappingConverter(
    XSSFSheet worksheet,
    IEnumerable<ICreateAccountRequest> accountRequests,
    IEnumerable<ICreateBudgetRequest> budgetRequests)
    : ExcelModelConverter<IAccountMapping, ICreateAccountMappingRequest>(worksheet)
{
    #region Fields

    /// <summary>
    /// Dictionary mapping an account name to its associated request
    /// </summary>
    private readonly Dictionary<string, ICreateAccountRequest> accountNameToRequest =
        accountRequests.ToDictionary(account => account.Name);

    /// <summary>
    /// Dictionary mapping a budget name to its association request
    /// </summary>
    private readonly Dictionary<string, ICreateBudgetRequest> budgetNameToRequest =
        budgetRequests.ToDictionary(budget => budget.Name);

    #endregion

    #region Properties

    /// <inheritdoc/>
    protected override Dictionary<string, Func<XSSFCell, object?>> ColumnConverters => new()
    {
        [nameof(IAccountMapping.AccountId)] = cell =>
            accountNameToRequest[DefaultColumnConverters.DefaultStringPropertyConverter(cell)].Id,
        [nameof(IAccountMapping.StartingBalance)] = DefaultColumnConverters.DefaultDecimalPropertyConverter,
        [nameof(IAccountMapping.BudgetId)] = DefaultColumnConverters.AsNullableConverter(cell =>
            budgetNameToRequest[DefaultColumnConverters.DefaultStringPropertyConverter(cell)].Id),
        [nameof(IAccountMapping.BudgetType)] = DefaultColumnConverters.AsNullableConverter(
                DefaultColumnConverters.DefaultEnumPropertyConverter<BudgetType>()),
        [nameof(IAccountMapping.IsDefault)] = DefaultColumnConverters.AsNullableConverter(
                DefaultColumnConverters.DefaultBooleanPropertyConverter)
    };

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override ICreateAccountMappingRequest GetDefaultCreateRequest() =>
        new CreateAccountMappingRequest()
        {
            AccountId = 0L,
            StartingBalance = 0.00m,
            BudgetId = null,
            BudgetType = null,
            IsDefault = true
        };

    #endregion
}