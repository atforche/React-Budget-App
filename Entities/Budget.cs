using System.Collections;

/// <summary>
/// Entity class representing a Budget.
/// Budgets are children of a Month and the parents of Transactions.
/// </summary>
public class Budget
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long BudgetId { get; set; }

    /// <summary>
    /// Name of the budget
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Type of the budget
    /// </summary>
    public BudgetType BudgetType { get; set; }

    /// <summary>
    /// Amount of the budget
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// For rolling budgets, the amount rolled over from the previous month
    /// </summary>
    public decimal? RolloverAmount { get; set; }

    /// <summary>
    /// For rolling budgets, has the override amount been overridden?
    /// </summary>
    public bool? IsRolloverAmountOverridden { get; set; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <summary>
    /// Navigation to the child Transactions
    /// </summary>
    public ICollection<Transaction> Transactions { get; } = null!;

    #endregion

    #region Methods

    #endregion
}

/// <summary>
/// Enum representing the different types of budgets
/// </summary>
public enum BudgetType
{
    Fixed,
    Rolling,
    Saving
}
