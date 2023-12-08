using System.Text.Json.Serialization;

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
/// Interface representing a request to create a Transaction.
/// </summary>
public interface ICreateTransactionRequest
{
    /// <summary>
    /// Id of the Create Transaction Request
    /// </summary>
    long Id { get; }

    /// <summary>
    /// The parent <see cref="IMonth.Id"/> or <see cref="ICreateMonthRequest.Id"/>
    /// If null, this request must fall under a <see cref="ICreateMonthRequest"/> and the value will be inferred.
    /// </summary>
    long? MonthId { get; }

    /// <inheritdoc cref="ITransaction.Date"/>
    DateTime Date { get; }

    /// <inheritdoc cref="ITransaction.Location"/>
    string Location { get; }

    /// <inheritdoc cref="ITransaction.Type"/>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    TransactionType Type { get; }

    /// <inheritdoc cref="ITransaction.Amount"/>
    decimal Amount { get; }

    /// <summary>
    /// The related <see cref="IAccount.Id"/> or <see cref="ICreateAccountRequest.Id"/>
    /// </summary>
    long? CreditCardAccountId { get; }

    /// <inheritdoc cref="ITransaction.PaidOffDate"/>
    DateTime? PaidOffDate { get; }

    /// <inheritdoc cref="ITransaction.TransactionApplications"/>
    ICollection<ICreateTransactionApplicationRequest> CreateTransactionApplicationRequests { get; }
}

public record CreateTransactionRequest : ICreateTransactionRequest
{
    private static long Sequence = long.MaxValue;

    /// <inheritdoc/>
    public long Id { get; } = Sequence--;

    /// <inheritdoc/>
    public long? MonthId { get; init; }

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
    public required ICollection<ICreateTransactionApplicationRequest> CreateTransactionApplicationRequests { get; init; }
}

/// <summary>
/// Interface representing a request to change a Transaction.
/// </summary>
public interface IChangeTransactionRequest
{
    /// <inheritdoc cref"ITransaction.Id"/>
    long Id { get; }

    /// <inheritdoc cref="ITransaction.Date"/>
    DateTime Date { get; }

    /// <inheritdoc cref="ITransaction.Location"/>
    string Location { get; }

    /// <inheritdoc cref="ITransaction.Type"/>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    TransactionType Type { get; }

    /// <inheritdoc cref="ITransaction.Amount"/>
    decimal Amount { get; }

    /// <inheritdoc cref="ITransaction.CreditCardAccountId"/>
    long? CreditCardAccountId { get; }

    /// <inheritdoc cref="ITransaction.PaidOffDate"/>
    DateTime? PaidOffDate { get; }

    /// <inheritdoc cref="ITransaction.TransactionApplications"/>
    ICollection<IChangeTransactionApplicationRequest> ChangeTransactionApplicationRequests { get; }
}

/// <summary>
/// Enum representing the different types of Transactions
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