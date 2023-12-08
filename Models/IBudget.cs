using System.Text.Json.Serialization;

namespace Models;

/// <summary>
/// Read-only interface representing a Budget.
/// </summary>
[ExcelTable("Budgets.{Month}")]
public interface IBudget
{
    /// <summary>
    /// Id of the Budget
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Id of the parent Month
    /// </summary>
    long MonthId { get; }

    /// <summary>
    /// Name of the Budget
    /// </summary>
    [ExcelColumn("Name", typeof(string))]
    string Name { get; }

    /// <summary>
    /// Type of the Budget
    /// </summary>
    [ExcelColumn("Type", typeof(string))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    BudgetType Type { get; }

    /// <summary>
    /// Amount of the Budget
    /// </summary>
    [ExcelColumn("Amount", typeof(decimal))]
    decimal Amount { get; }

    /// <summary>
    /// Rollover Amount of the Budget
    /// </summary>
    /// <remarks>
    /// If this Budget has a type of Rolling or Savings, the starting balance of this Budget is equal to
    /// the Rollover Amount from last month plus the budgeted amount for this month. 
    /// </remarks>
    [ExcelColumn("Rollover From Last Month", typeof(decimal), true)]
    decimal? RolloverAmount { get; }

    /// <summary>
    /// Is Rollover Amount Override flag of the Budget
    /// </summary>
    /// <remarks>
    /// If this Budget has a type of Rolling or Savings, there are some cases where the Rollover Amount from
    /// the previous month should not be used going forward. This flag indicates that the amount stored in 
    /// Rollover Amount has been overridden with an arbitrary value, and it does not come from the previous month.
    /// </remarks>
    [ExcelColumn("Override Rollover Amount", typeof(bool), true)]
    bool? IsRolloverAmountOverridden { get; }
}

/// <summary>
/// Interface representing a request to create a Budget.
/// </summary>
public interface ICreateBudgetRequest
{
    /// <summary>
    /// Id of the Create Budget Request
    /// </summary>
    long Id { get; }

    /// <summary>
    /// The parent <see cref="IMonth.Id"/> or <see cref="ICreateMonthRequest.Id"/>
    /// If null, this request must fall under a <see cref="ICreateMonthRequest"/> and the value will be inferred.
    /// </summary>
    long? MonthId { get; }

    /// <inheritdoc cref="IBudget.Name"/>
    string Name { get; }

    /// <inheritdoc cref="IBudget.Type"/>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    BudgetType Type { get; }

    /// <inheritdoc cref="IBudget.Amount"/>
    decimal Amount { get; }

    /// <inheritdoc cref="IBudget.RolloverAmount"/>
    decimal? RolloverAmount { get; }

    /// <inheritdoc cref="IBudget.IsRolloverAmountOverridden"/>
    bool? IsRolloverAmountOverridden { get; }
}

public record CreateBudgetRequest : ICreateBudgetRequest
{
    private static long Sequence = 1;

    /// <inheritdoc/>
    public long Id { get; } = Sequence++;

    /// <inheritdoc/>
    public long? MonthId { get; init; }

    /// <inheritdoc/>
    public required string Name { get; init; }

    /// <inheritdoc/>
    public required BudgetType Type { get; init; }

    /// <inheritdoc/>
    public required decimal Amount { get; init; }

    /// <inheritdoc/>
    public required decimal? RolloverAmount { get; init; }

    /// <inheritdoc/>
    public required bool? IsRolloverAmountOverridden { get; init; }
}

/// <summary>
/// Interface representing a request to change a Budget.
/// </summary>
public interface IChangeBudgetRequest
{
    /// <inheritdoc cref="IBudget.Id"/>
    long Id { get; }

    /// <inheritdoc cref="IBudget.Name"/>
    string Name { get; }

    /// <inheritdoc cref="IBudget.Type"/>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    BudgetType Type { get; }

    /// <inheritdoc cref="IBudget.Amount"/>
    decimal Amount { get; }

    /// <inheritdoc cref="IBudget.RolloverAmount"/>
    decimal? RolloverAmount { get; }

    /// <inheritdoc cref="IBudget.IsRolloverAmountOverridden"/>
    bool? IsRolloverAmountOverridden { get; }
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