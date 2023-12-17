using System.Text.Json.Serialization;

namespace Models.ChangeRequests;

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
/// Record class representing a request to change a Budget.
/// </summary>
public record ChangeBudgetRequest : IChangeBudgetRequest
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required string Name { get; init; }

    /// <inheritdoc/>
    public required BudgetType Type { get; init; }

    /// <inheritdoc/>
    public required decimal Amount { get; init; }

    /// <inheritdoc/>
    public decimal? RolloverAmount { get; init; }

    /// <inheritdoc/>
    public bool? IsRolloverAmountOverridden { get; init; }
}