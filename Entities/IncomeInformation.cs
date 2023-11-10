namespace Entities;

/// <summary>
/// Entity class representing an Income Information.
/// Income Information are children of Expected Income Rates and Income Dates.
/// </summary>
public class IncomeInformation
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

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

    #region Navigations

    /// <summary>
    /// Navigation to the parent Expected Income Rate
    /// </summary>
    /// <remarks>
    /// If not null, this Income Information belongs to an Expected Income Rate.
    /// An Income Information can only belong to one of an Expected Income Rate or an Income Date.
    /// </remarks>
    public ExpectedIncomeRate? ExpectedIncomeRate { get; } = null!;

    /// <summary>
    /// Navigation to the parent Income Date
    /// </summary>
    /// <remark>
    /// If not null, this Income Information belongs to an Income Date.
    /// An Income Information can only belong to one of an Expected Income Rate or an Income Date.
    /// </remark>
    public IncomeDate? IncomeDate { get; } = null!;

    #endregion

    #region Methods

    #endregion
}