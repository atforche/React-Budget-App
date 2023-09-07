using System.Collections;

namespace Entities;

/// <summary>
/// Entity class representing an Employer.
/// Employers are children of a Month and parents to Expected Income Rates,
/// Expected Income Dates, and Actual Incomes.
/// </summary>
public class Employer
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long EmployerId { get; set; }

    /// <summary>
    /// Name of the employer
    /// </summary>
    public string Employer { get; set; } = null!;

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <summary>
    /// Navigation to the child Expected Income Rate
    /// </summary>
    public ExpectedIncomeRate ExpectedIncomeRate { get; set; } = null!;

    /// <summary>
    /// Navigation to the child Expected Income Dates
    /// </summary>
    public ICollection<ExpectedIncomeDate> ExpectedIncomeDates { get; set; } = null!;

    /// <summary>
    /// Navigation to the child Actual Incomes
    /// </summary>
    public ICollection<ActualIncome> ActualIncomes { get; set; } = null!;

    #endregion

    #region Methods

    #endregion
}
