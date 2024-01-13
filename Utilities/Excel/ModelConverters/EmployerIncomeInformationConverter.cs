using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Employer Income Information models to and from Excel format.
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
/// <param name="parentType">If this converter deals with child models, the parent type of this model converter</param>
public class EmployerIncomeInformationConverter(XSSFSheet worksheet, Type? parentType)
    : ExcelModelConverter<IEmployerIncomeInformation, ICreateEmployerIncomeInformationRequest>(worksheet, parentType)
{
    #region Properties

    /// <inheritdoc/>
    protected override Dictionary<string, Func<XSSFCell, object?>> ColumnConverters =>
        new()
        {
            [nameof(IEmployerIncomeInformation.SalaryIncome)] =
                DefaultColumnConverters.DefaultDecimalPropertyConverter,
            [nameof(IEmployerIncomeInformation.AdditionalTaxableIncome)] =
                DefaultColumnConverters.DefaultDecimalPropertyConverter,
            [nameof(IEmployerIncomeInformation.RetirementContributionAmount)] =
                DefaultColumnConverters.DefaultDecimalPropertyConverter,
            [nameof(IEmployerIncomeInformation.PensionContributionAmount)] =
                DefaultColumnConverters.DefaultDecimalPropertyConverter,
            [nameof(IEmployerIncomeInformation.PreTaxDeductionAmount)] =
                DefaultColumnConverters.DefaultDecimalPropertyConverter,
            [nameof(IEmployerIncomeInformation.TaxWithholdingAmount)] =
                DefaultColumnConverters.DefaultDecimalPropertyConverter,
            [nameof(IEmployerIncomeInformation.PostTaxDeductionAmount)] =
                DefaultColumnConverters.DefaultDecimalPropertyConverter,
        };

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override ICreateEmployerIncomeInformationRequest GetDefaultCreateRequest() =>
        new CreateEmployerIncomeInformationRequest()
        {
            SalaryIncome = 0.00m,
            AdditionalTaxableIncome = 0.00m,
            RetirementContributionAmount = 0.00m,
            PensionContributionAmount = 0.00m,
            PreTaxDeductionAmount = 0.00m,
            TaxWithholdingAmount = 0.00m,
            PostTaxDeductionAmount = 0.00m
        };

    #endregion
}