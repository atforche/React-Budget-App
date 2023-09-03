using System;

/// <summary>
/// Entity class representing an Expected Income Date.
/// Expected Income Dates are children of an Expected Income Rate.
/// </summary>
public class ExpectedIncomeDate
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long ExpectedIncomeDateId { get; set; }

    /// <summary>
    /// Date of the expected income date
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Employer Name of the expected income date
    /// </summary>
    public string Employer { get; set; } = null!;

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Expected Income Rate
    /// </summary>
    public ExpectedIncomeRate ExpectedIncomeRate { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
