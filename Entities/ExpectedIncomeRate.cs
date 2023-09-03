using System.Collections;

/// <summary>
/// Entity class representing an Expected Income Rate.
/// Expected Income Rates are children of a Month.
/// </summary>
public class ExpectedIncomeRate : Income
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long ExpectedIncomeRateId { get; set; }

    /// <summary>
    /// Percentage representing the Retirement Contributions of the expected income rate
    /// </summary>
    public decimal RetirementContributionRate { get; set; }

    /// <summary>
    /// Percentage representing the Tax Rate of this expected income rate
    /// </summary>
    public decimal TaxRate { get; set; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the children Expected Income Dates
    /// </summary>
    public ICollection<ExpectedIncomeDate> ExpectedIncomeDates { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
