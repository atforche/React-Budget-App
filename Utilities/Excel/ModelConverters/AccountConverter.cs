using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Account models to and from Excel format.
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
public class AccountConverter(XSSFSheet worksheet) : ExcelModelConverter<IAccount, ICreateAccountRequest>(worksheet)
{
    #region Properties

    /// <inheritdoc/>
    protected override Dictionary<string, Func<XSSFCell, object?>> ColumnConverters => new()
    {
        [nameof(IAccount.Name)] = DefaultColumnConverters.DefaultStringPropertyConverter,
        [nameof(IAccount.Type)] = DefaultColumnConverters.DefaultEnumPropertyConverter<AccountType>()
    };

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override ICreateAccountRequest GetDefaultCreateRequest() =>
        new CreateAccountRequest()
        {
            Name = "",
            Type = AccountType.Regular
        };

    #endregion
}