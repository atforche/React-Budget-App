namespace Entities;

/// <summary>
/// Entity class representing an Employer Income Information.
/// Employer Income Information are children of an Employer Income Rate or an Income.
/// </summary>
public class EmployerIncomeInformation
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Income
    /// </summary>
    /// <remark>
    /// If not null, this Income Information belongs to an Income.
    /// An Employer Income Information can only belong to one of an Employer Income Rate or an Income.
    /// </remark>
    public Income? Income { get; } = null!;

    /// <summary>
    /// Navigation to the parent Employer Income Rate
    /// </summary>
    /// <remarks>
    /// If not null, this Income Information belongs to an Employer Income Rate.
    /// An Employer Income Information can only belong to one of an Employer Income Rate or an Income.
    /// </remarks>
    public EmployerIncomeRate? EmployerIncomeRate { get; } = null!;

    /// <summary>
    /// Salary Income for this Income Information
    /// </summary>
    public decimal SalaryIncome { get; }

    /// <summary>
    /// Additional Taxable Income for this Income Information
    /// </summary>
    public decimal AdditionalTaxableIncome { get; }

    /// <summary>
    /// Retirement Contribution Amount for this Income Information
    /// </summary>
    public decimal RetirementContributionAmount { get; }

    /// <summary>
    /// Pension Contribution Amount for this Income Information
    /// </summary>
    public decimal PensionContributionAmount { get; }

    /// <summary>
    /// Pre-Tax Deduction Amount for this Income Information
    /// </summary>
    public decimal PreTaxDeductionAmount { get; }

    /// <summary>
    /// Tax Withholding Amount for this Income Information
    /// </summary>
    public decimal TaxWithholdingAmount { get; }

    /// <summary>
    /// Post-Tax Deduction Amount for this Income Information
    /// </summary>
    public decimal PostTaxDeductionAmount { get; }

    #endregion

    #region Methods

    #endregion
}