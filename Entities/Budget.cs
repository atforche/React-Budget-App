namespace Entities;

/// <summary>
/// Entity class representing a Budget.
/// Budgets are children of a Month, and parents of Transaction Applications and Account Mappings.
/// </summary>
public class Budget
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Name of this Budget
    /// </summary>
    public string Name { get; } = null!;

    /// <summary>
    /// Type of this Budget
    /// </summary>
    public BudgetType BudgetType { get; }

    /// <summary>
    /// Amount of this Budget
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Rollover Amount of this Budget
    /// </summary>
    /// <remarks>
    /// If this Budget has a type of Rolling or Savings, the starting balance of this Budget is equal to
    /// the Rollover Amount from last month plus the budgeted amount for this month. 
    /// </remarks>
    public decimal? RolloverAmount { get; }

    /// <summary>
    /// Is Rollover Amount Override flag of this Budget
    /// </summary>
    /// <remarks>
    /// If this Budget has a type of Rolling or Savings, there are some cases where the Rollover Amount from
    /// the previous month should not be used going forward. This flag indicates that the amount stored in 
    /// RolloverAmount has been overridden with an arbitrary value, and it does not come from the previous month.
    /// </remarks>
    public bool? IsRolloverAmountOverridden { get; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <summary>
    /// Navigation to the child Transaction Applications
    /// </summary>
    public ICollection<TransactionApplication> TransactionApplications { get; } = null!;

    /// <summary>
    /// Navigation to the child Account Mapping
    /// </summary>
    /// <remarks>
    /// If not null, there's an Account Mapping that maps directly to this individual budget.
    /// </remarks>
    public AccountMapping? AccountMapping { get; } = null!;

    #endregion

    #region Methods

    #endregion
}

/// <summary>
/// Enum representing the different types of Budgets
/// </summary>
public enum BudgetType
{
    /// <summary>
    /// A Fixed Budget has a fixed amount to spend each month. This amount does not rollover month to month, so any
    /// over or under spending from the previous month does not affect subsequent months. These Budgets generally
    /// represent essential expenses that should not be punished for overspending.
    /// </summary>
    Fixed,

    /// <summary>
    /// A Weekly Budget is very similar to a Fixed Budget. The main difference is that a Weekly Budget has a fixed
    /// amount to spend each week, instead of a fixed amount for the entire month. The total monthly amount of this
    /// budget is equal to the Amount property multiplied by the number of weeks in the month. The number of weeks is 
    /// calculated by counting the number of Sundays in the given month.
    /// </summary>
    Weekly,

    /// <summary>
    /// A Rolling Budget has a balance that rolls over from month to month. If the budget isn't completely
    /// spent during a month, then the budget will have more than the budgeted amount to spend next month. 
    /// If the budget is exceeded during a month, then the budget will have less than the budgeted amount to spend
    /// next month. This changing rollover amount carries forward indefinitely, unless it is overridden for a 
    /// particular month.
    /// </summary>
    Rolling,

    /// <summary>
    /// A Savings Budget is very similar to a Rolling Budget. The main difference is that the Amount on a 
    /// Savings Budget specifies the amount of credits that are budgeted, instead of the amount of debits.
    /// For example, a Savings Budget with an Amount of $200 means that we have budgeted for $200 in credits. 
    /// Any debits against a Savings Budget increase the credit amount needed to hit the budget. 
    /// </summary>
    Saving
}
