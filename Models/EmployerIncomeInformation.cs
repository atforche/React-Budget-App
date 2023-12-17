using Models.Attributes;

namespace Models;

/// <summary>
/// Read-only interface representing an Employer Income Information.
/// </summary>
[ExcelTable("EmployerIncomeRates.{Month}")]
[ExcelTable("Incomes.{Month}")]
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
/// Record class representing an Employer Income Information.
/// </summary>
public record EmployerIncomeInformation : IEmployerIncomeInformation
{
    /// <inheritdoc/>
    public required long Id { get; init; }

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