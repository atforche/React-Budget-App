namespace Models.CreateRequests;

/// <summary>
/// Interface representing a request to create an Account Balance.
/// </summary>
public interface ICreateAccountBalanceRequest
{
    /// <summary>
    /// Id of the Create Account Balance Request
    /// </summary>
    long Id { get; }

    /// <summary>
    /// The parent <see cref="IAccount.Id"/> or <see cref="ICreateAccountRequest.Id"/>
    /// </summary>
    long AccountId { get; }

    /// <summary>
    /// The parent <see cref="IMonth.Id"/> or <see cref="ICreateMonthRequest.Id"/>
    /// If null, this request must fall under a <see cref="ICreateMonthRequest"/> and the value will be inferred.
    /// </summary>
    long? MonthId { get; }

    /// <inheritdoc cref="IAccountBalance.Date"/>
    DateTime Date { get; }

    /// <inheritdoc cref="IAccountBalance.Amount"/>
    decimal Amount { get; }
}

/// <summary>
/// Record class representing a request to create an Account Balance.
/// </summary>
public record CreateAccountBalanceRequest : CreateRequestBase, ICreateAccountBalanceRequest
{
    /// <inheritdoc/>
    public required long AccountId { get; init; }

    /// <inheritdoc/>
    public long? MonthId { get; init; }

    /// <inheritdoc/>
    public required DateTime Date { get; init; }

    /// <inheritdoc/>
    public required decimal Amount { get; init; }
}