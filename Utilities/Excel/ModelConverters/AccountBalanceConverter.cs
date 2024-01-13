using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Account Balance models to and from Excel format.
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
/// <param name="accountRequests">List of converted account models</param>
public class AccountBalanceConverter(XSSFSheet worksheet, IEnumerable<ICreateAccountRequest> accountRequests)
    : ExcelModelConverter<IAccountBalance, ICreateAccountBalanceRequest>(worksheet)
{
    #region Fields

    /// <summary>
    /// Dictionary mapping an account name to its associated request
    /// </summary>
    private readonly Dictionary<string, ICreateAccountRequest> accountNameToRequest =
        accountRequests.ToDictionary(account => account.Name);

    #endregion

    #region Properties

    /// <inheritdoc/>
    protected override Dictionary<string, Func<XSSFCell, object?>> ColumnConverters => new()
    {
        [nameof(IAccountBalance.AccountId)] =
                cell => accountNameToRequest[DefaultColumnConverters.DefaultStringPropertyConverter.Invoke(cell)].Id,
        [nameof(IAccountBalance.Date)] = DefaultColumnConverters.DefaultDateTimePropertyConverter,
        [nameof(IAccountBalance.Amount)] = DefaultColumnConverters.DefaultDecimalPropertyConverter
    };

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override ICreateAccountBalanceRequest GetDefaultCreateRequest() =>
        new CreateAccountBalanceRequest()
        {
            AccountId = 0L,
            Date = DateTime.Today,
            Amount = 0.00m
        };

    #endregion
}