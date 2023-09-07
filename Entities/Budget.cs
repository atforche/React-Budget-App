using System.Collections;

namespace Entities;

/// <summary>
/// Entity class representing a Budget.
/// Budgets are children of a Month and the parents of Transactions.
/// </summary>
[ExcelTable(TableName = "Budgets")]
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
    [ExcelColumn(ColumnName = "Name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Type of the budget
    /// </summary>
    [ExcelColumn(ColumnName = "Type")]
    public BudgetType BudgetType { get; set; }

    /// <summary>
    /// Amount of the budget
    /// </summary>
    [ExcelColumn(ColumnName = "Amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// For rolling budgets, the amount rolled over from the previous month
    /// </summary>
    [
        ExcelColumn(ColumnName = "Rollover From Last Month"),
        ExcelColumn(ColumnName = "Override Rollover Amount")
    ]
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
