using Models.Attributes;

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
/// Record class representing an Account Balance.
/// </summary>
public record AccountBalance : IAccountBalance
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required long AccountId { get; init; }

    /// <inheritdoc/>
    public required long MonthId { get; init; }

    /// <inheritdoc/>
    public required DateTime Date { get; init; }

    /// <inheritdoc/>
    public required decimal Amount { get; init; }
}