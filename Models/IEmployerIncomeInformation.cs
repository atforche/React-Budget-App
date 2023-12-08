namespace Models;

/// <summary>
/// Read-only interface representing an Employer Income Information.
/// </summary>
[ExcelTable("EmployerIncomeRates.{Month}", parentType: typeof(IEmployerIncomeRate))]
[ExcelTable("Incomes.{Month}", parentType: typeof(IIncome))]
public interface IEmployerIncomeInformation
{
    /// <summary>
    /// Id of the Employer Income Information
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Id of the parent Income
    /// </summary>
    long? IncomeId { get; }

    /// <summary>
    /// Id of the parent Employer Income Rate
    /// </summary>
    long? EmployerIncomeRateId { get; }

    /// <summary>
    /// Salary Income for this Income Information
    /// </summary>
    [ExcelColumn("Salary Income", typeof(decimal))]
    decimal SalaryIncome { get; }

    /// <summary>
    /// Additional Taxable Income for this Income Information
    /// </summary>
    [ExcelColumn("Additional Taxable Income", typeof(decimal))]
    decimal AdditionalTaxableIncome { get; }

    /// <summary>
    /// Retirement Contribution Amount for this Income Information
    /// </summary>
    [ExcelColumn("Retirement Contribution Amount", typeof(decimal))]
    decimal RetirementContributionAmount { get; }

    /// <summary>
    /// Pension Contribution Amount for this Income Information
    /// </summary>
    [ExcelColumn("Pension Contribution Amount", typeof(decimal))]
    decimal PensionContributionAmount { get; }

    /// <summary>
    /// Pre-Tax Deduction Amount for this Income Information
    /// </summary>
    [ExcelColumn("Pre-Tax Deductions", typeof(decimal))]
    decimal PreTaxDeductionAmount { get; }

    /// <summary>
    /// Tax Withholding Amount for this Income Information
    /// </summary>
    [ExcelColumn("Tax Withholding Amount", typeof(decimal))]
    decimal TaxWithholdingAmount { get; }

    /// <summary>
    /// Post-Tax Deduction Amount for this Income Information
    /// </summary>
    [ExcelColumn("Post-Tax Deductions", typeof(decimal))]
    decimal PostTaxDeductionAmount { get; }
}

/// <summary>
/// Interface representing a request to create an Employer Income Information.
/// </summary>
public interface ICreateEmployerIncomeInformationRequest
{
    /// <summary>
    /// Id of the Create Employer Income Information Request
    /// </summary>
    long Id { get; }

    /// <summary>
    /// The parent <see cref="IIncome.Id"/> or <see cref="ICreateIncomeRequest.Id"/>
    /// If null and this request falls under a <see cref="ICreateIncomeRequest"/>, the value will be inferred.
    /// </summary>
    long? IncomeId { get; }

    /// <summary>
    /// The parent <see cref="IEmployerIncomeRate.Id"/> or <see cref="ICreateEmployerIncomeRateRequest.Id"/>
    /// If null and this request falls under a <see cref="ICreateEmployerIncomeRateRequest"/>, the value will be inferred.
    /// </summary>
    long? EmployerIncomeRateId { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.SalaryIncome"/>
    decimal SalaryIncome { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.AdditionalTaxableIncome"/>
    decimal AdditionalTaxableIncome { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.RetirementContributionAmount"/>
    decimal RetirementContributionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PensionContributionAmount"/>
    decimal PensionContributionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PreTaxDeductionAmount"/>
    decimal PreTaxDeductionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.TaxWithholdingAmount"/>
    decimal TaxWithholdingAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PostTaxDeductionAmount"/>
    decimal PostTaxDeductionAmount { get; }
}

public record CreateEmployerIncomeInformationRequest : ICreateEmployerIncomeInformationRequest
{
    private static long Sequence = long.MaxValue;

    /// <inheritdoc/>
    public long Id { get; } = Sequence--;

    /// <inheritdoc/>
    public long? IncomeId { get; init; }

    /// <inheritdoc/>  
    public long? EmployerIncomeRateId { get; init; }

    /// <inheritdoc/> 
    public required decimal SalaryIncome { get; init; }

    /// <inheritdoc/> 
    public required decimal AdditionalTaxableIncome { get; init; }

    /// <inheritdoc/>
    public required decimal RetirementContributionAmount { get; init; }

    /// <inheritdoc/> 
    public required decimal PensionContributionAmount { get; init; }

    /// <inheritdoc/>
    public required decimal PreTaxDeductionAmount { get; init; }

    /// <inheritdoc/>
    public required decimal TaxWithholdingAmount { get; init; }

    /// <inheritdoc/>
    public required decimal PostTaxDeductionAmount { get; init; }
}

/// <summary>
/// Interface representing a request to change an Employer Income Information.
/// </summary>
public interface IChangeEmployerIncomeInformationRequest
{
    /// <inheritdoc cref="IEmployerIncomeInformation.Id"/>
    long Id { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.SalaryIncome"/>
    decimal SalaryIncome { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.AdditionalTaxableIncome"/>
    decimal AdditionalTaxableIncome { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.RetirementContributionAmount"/>
    decimal RetirementContributionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PensionContributionAmount"/>
    decimal PensionContributionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PreTaxDeductionAmount"/>
    decimal PreTaxDeductionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.TaxWithholdingAmount"/>
    decimal TaxWithholdingAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PostTaxDeductionAmount"/>
    decimal PostTaxDeductionAmount { get; }
}