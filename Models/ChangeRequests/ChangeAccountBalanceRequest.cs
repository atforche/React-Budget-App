namespace Models.ChangeRequests;

/// <summary>
/// Interface representing a request to change an Account Balance.
/// </summary>
public interface IChangeAccountBalanceRequest
{
    /// <inheritdoc cref="IAccountBalance.Id"/>
    long Id { get; }

    /// <inheritdoc cref="IAccountBalance.Date"/>
    DateTime Date { get; }

    /// <inheritdoc cref="IAccountBalance.Date"/>
    decimal Amount { get; }
}

/// <summary>
/// Record class representing a request to change an Account Balance.
/// </summary>
public record ChangeAccountBalanceRequest : IChangeAccountBalanceRequest
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required DateTime Date { get; init; }

    /// <inheritdoc/>
    public required decimal Amount { get; init; }
}