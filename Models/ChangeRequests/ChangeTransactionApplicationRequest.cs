namespace Models.ChangeRequests;

/// <summary>
/// Interface representing a request to change a Transaction Application.
/// </summary>
public interface IChangeTransactionApplicationRequest
{
    /// <inheritdoc cref="ITransactionApplication.Id"/>
    long Id { get; }

    /// <inheritdoc cref="ITransactionApplication.Type"/>
    ApplicationType Type { get; }

    /// <inheritdoc cref="ITransactionApplication.Description"/>
    string Description { get; }

    /// <inheritdoc cref="ITransactionApplication.Amount"/>
    decimal Amount { get; }

    /// <inheritdoc cref="ITransactionApplication.BudgetId"/>
    long BudgetId { get; }

    /// <inheritdoc cref="ITransactionApplication.OverrideAccountMappingId"/>
    long? OverrideAccountMappingId { get; }
}

/// <summary>
/// Record class representing a request to change a Transaction Application.
/// </summary>
public record ChangeTransactionApplicationRequest : IChangeTransactionApplicationRequest
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required ApplicationType Type { get; init; }

    /// <inheritdoc/>
    public required string Description { get; init; }

    /// <inheritdoc/>
    public required decimal Amount { get; init; }

    /// <inheritdoc/>
    public required long BudgetId { get; init; }

    /// <inheritdoc/>
    public long? OverrideAccountMappingId { get; init; }
}