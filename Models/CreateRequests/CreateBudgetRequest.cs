namespace Models.CreateRequests;

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
    long? MonthId { get; init; }

    /// <inheritdoc cref="IBudget.Name"/>
    string Name { get; init; }

    /// <inheritdoc cref="IBudget.Type"/>
    BudgetType Type { get; init; }

    /// <inheritdoc cref="IBudget.Amount"/>
    decimal Amount { get; init; }

    /// <inheritdoc cref="IBudget.RolloverAmount"/>
    decimal? RolloverAmount { get; init; }

    /// <inheritdoc cref="IBudget.IsRolloverAmountOverridden"/>
    bool? IsRolloverAmountOverridden { get; init; }
}

/// <summary>
/// Record class representing a request to create a Budget.
/// </summary>
public record CreateBudgetRequest : CreateRequestBase, ICreateBudgetRequest
{
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