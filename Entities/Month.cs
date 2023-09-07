using System.Collections;

namespace Entities;

/// <summary>
/// Entity class representing a Month.
/// A Month is the parent entity of any data that occurs during a given month.
/// </summary>
public class Month
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long MonthId { get; set; }

    /// <summary>
    /// Year of the month
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Number representing the month (1-12)
    /// </summary>
    public int MonthNumber { get; set; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the child Budgets
    /// </summary>
    public ICollection<Budget> Budgets { get; } = null!;

    /// <summary>
    /// Navigation to the child Account Balances
    /// </summary>
    public ICollection<AccountBalance> AccountBalances { get; } = null!;

    /// <summary>
    /// Navigation to the child Employers
    /// </summary>
    public ICollection<Employer> Employers { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
