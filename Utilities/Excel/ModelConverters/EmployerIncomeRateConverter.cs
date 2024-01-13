using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Employer Income Rate models to and from Excel format.
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
/// <param name="employerRequests">List of converted employer models</param>
public class EmployerIncomeRateConverter(XSSFSheet worksheet, IEnumerable<ICreateEmployerRequest> employerRequests)
    : ExcelModelConverter<IEmployerIncomeRate, ICreateEmployerIncomeRateRequest>(worksheet)
{
    #region Fields

    /// <summary>
    /// Dictionary mapping an employer name to its associated request
    /// </summary>
    private readonly Dictionary<string, ICreateEmployerRequest> employerNameToRequest =
        employerRequests.ToDictionary(employer => employer.Name);

    /// <summary>
    /// Child model converter for Employer Income Information
    /// </summary>
    private readonly EmployerIncomeInformationConverter employerIncomeInformationConverter =
        new(worksheet, typeof(IEmployerIncomeRate));

    #endregion

    #region Properties

    /// <inheritdoc/>
    protected override Dictionary<string, Func<XSSFCell, object?>> ColumnConverters =>
        new()
        {
            [nameof(IEmployerIncomeRate.EmployerId)] = cell =>
                employerNameToRequest[DefaultColumnConverters.DefaultStringPropertyConverter(cell)].Id,
        };

    /// <inheritdoc/>
    protected override Dictionary<string, IExcelModelConverter> ChildModelConverters =>
        new()
        {
            [nameof(IEmployerIncomeRate.EmployerIncomeInformation)] = employerIncomeInformationConverter
        };

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override ICreateEmployerIncomeRateRequest GetDefaultCreateRequest() =>
        new CreateEmployerIncomeRateRequest()
        {
            EmployerId = 0L,
            EmployerIncomeInformation = employerIncomeInformationConverter.GetDefaultCreateRequest()
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