namespace Models;

/// <summary>
/// Read-only interface representing an Account Balance.
/// </summary>
[ExcelTable("AccountBalances.{Month}")]
public interface IAccountBalance
{
    /// <summary>
    /// Id of the Account Balance
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Id of the parent Account
    /// </summary>
    [ExcelColumn("Account Name", typeof(string))]
    long AccountId { get; }

    /// <summary>
    /// Id of the parent Month
    /// </summary>
    long MonthId { get; }

    /// <summary>
    /// Date of the Account Balance
    /// </summary>
    [ExcelColumn("Date", typeof(DateTime))]
    DateTime Date { get; }

    /// <summary>
    /// Amount of the Account Balance
    /// </summary>
    [ExcelColumn("Amount", typeof(decimal))]
    decimal Amount { get; }
}

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

public record CreateAccountBalanceRequest : ICreateAccountBalanceRequest
{
    private static long Sequence = long.MaxValue;

    /// <inheritdoc/>
    public long Id { get; } = Sequence--;

    /// <inheritdoc/>
    public required long AccountId { get; init; }

    /// <inheritdoc/>
    public long? MonthId { get; init; }

    /// <inheritdoc/>
    public required DateTime Date { get; init; }

    /// <inheritdoc/>
    public required decimal Amount { get; init; }
}

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