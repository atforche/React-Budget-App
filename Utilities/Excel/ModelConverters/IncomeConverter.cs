using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Income models to and from Excel format.
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
/// <param name="employerRequests">List of converted employer models</param>
/// <param name="accountRequests">List of converted account models</param>
public class IncomeConverter(
    XSSFSheet worksheet,
    IEnumerable<ICreateEmployerRequest> employerRequests,
    IEnumerable<ICreateAccountRequest> accountRequests)
    : ExcelModelConverter<IIncome, ICreateIncomeRequest>(worksheet)
{
    #region Fields

    /// <summary>
    /// Dictionary mapping an employer name to its associated request
    /// </summary>
    private readonly Dictionary<string, ICreateEmployerRequest> employerNameToRequest =
        employerRequests.ToDictionary(employer => employer.Name);

    /// <summary>
    /// Dictionary mapping an account name to its associated request
    /// </summary>
    private readonly Dictionary<string, ICreateAccountRequest> accountNameToRequest =
        accountRequests.ToDictionary(account => account.Name);

    /// <summary>
    /// Child model converter for Employer Income Information
    /// </summary>
    private readonly EmployerIncomeInformationConverter employerIncomeInformationConverter =
        new(worksheet, typeof(IIncome));

    #endregion

    #region Properties

    /// <inheritdoc/>
    protected override Dictionary<string, Func<XSSFCell, object?>> ColumnConverters =>
        new()
        {
            [nameof(IIncome.EmployerId)] = cell =>
                employerNameToRequest[DefaultColumnConverters.DefaultStringPropertyConverter(cell)].Id,
            [nameof(IIncome.Date)] = DefaultColumnConverters.DefaultDateTimePropertyConverter,
            [nameof(IIncome.Description)] = DefaultColumnConverters.DefaultStringPropertyConverter,
            [nameof(IIncome.Amount)] = DefaultColumnConverters.DefaultDecimalPropertyConverter,
            [nameof(IIncome.OverrideAccountMappingId)] = DefaultColumnConverters.AsNullableConverter(
                cell => accountNameToRequest[DefaultColumnConverters.DefaultStringPropertyConverter(cell)].Id),
        };

    /// <inheritdoc/>
    protected override Dictionary<string, IExcelModelConverter> ChildModelConverters =>
        new()
        {
            [nameof(IIncome.EmployerIncomeInformation)] = employerIncomeInformationConverter
        };

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override ICreateIncomeRequest GetDefaultCreateRequest() =>
        new CreateIncomeRequest()
        {
            Date = DateTime.Today,
            Amount = 0.00m,
            Description = "",
            OverrideAccountMappingId = null,
            EmployerIncomeInformation = null
        };

    /// <inheritdoc/>
    protected override ExcelRowRange? GetChildModelIndexes(Type childType, XSSFTable table)
    {
        if (childType == typeof(IEmployerIncomeInformation))
        {
            return new ExcelRowRange(CurrentIndex, CurrentIndex);
        }
        return null;
    }

    #endregion
}