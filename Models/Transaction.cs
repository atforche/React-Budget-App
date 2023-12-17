using System.Text.Json.Serialization;
using Models.Attributes;

namespace Models;

/// <summary>
/// Read-only interface representing a Transaction.
/// </summary>
[ExcelTable("Transactions.{Month}")]
public interface ITransaction
{
    /// <summary>
    /// Id of the Transaction
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Id of the parent Month
    /// </summary>
    long MonthId { get; }

    /// <summary>
    /// Date of the Transaction
    /// </summary>
    [ExcelColumn("Date", typeof(DateTime), true)]
    DateTime Date { get; }

    /// <summary>
    /// Location of the Transaction
    /// </summary>
    [ExcelColumn("Location", typeof(string), true)]
    string Location { get; }

    /// <summary>
    /// Type of the Transaction
    /// </summary>
    [ExcelColumn("Type", typeof(string))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    TransactionType Type { get; }

    /// <summary>
    /// Amount of the Transaction
    /// </summary>
    [ExcelColumn("Amount", typeof(decimal))]
    decimal Amount { get; }

    /// <summary>
    /// Id of the related Credit Card Account
    /// </summary>
    /// <remark>
    /// If not null, this Transaction was made on a credit card. Any Applications under this Transaction should
    /// count against their Budgets but not decrement their mapped Accounts until this Transaction is paid off.
    /// </remark>
    [ExcelColumn("Credit Card Account", typeof(string), true)]
    long? CreditCardAccountId { get; }

    /// <summary>
    /// Paid Off Date of the Transaction
    /// </summary>
    /// <remarks>
    /// For a credit card Transaction, this date indicates when the credit card balance for this transaction was 
    /// paid off.
    /// </remarks>
    [ExcelColumn("Payoff Date", typeof(DateTime), true)]
    DateTime? PaidOffDate { get; }

    /// <summary>
    /// Child Transaction Applications
    /// </summary>
    ICollection<ITransactionApplication> TransactionApplications { get; }
}

/// <summary>
/// Record class representing a Transaction.
/// </summary>
public record Transaction : ITransaction
{
    /// <inheritdoc/> 
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required long MonthId { get; init; }

    /// <inheritdoc/>
    public required DateTime Date { get; init; }

    /// <inheritdoc/>
    public required string Location { get; init; }

    /// <inheritdoc/>
    public required TransactionType Type { get; init; }

    /// <inheritdoc/>
    public required decimal Amount { get; init; }

    /// <inheritdoc/>
    public long? CreditCardAccountId { get; init; }

    /// <inheritdoc/>
    public DateTime? PaidOffDate { get; init; }

    /// <inheritdoc/>
    public required ICollection<ITransactionApplication> TransactionApplications { get; init; }
}

/// <summary>
/// Enum representing the different types of Transactions.
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// A Debit Transaction represents money that was spent against a budget
    /// </summary>
    Debit,

    /// <summary>
    /// A Credit Transaction represents money that was saved / put back towards a budget
    /// </summary>
    Credit,

    /// <summary>
    /// A Transfer Transaction represents money that is debited from one budget and credited to another
    /// </summary>
    Transfer
}