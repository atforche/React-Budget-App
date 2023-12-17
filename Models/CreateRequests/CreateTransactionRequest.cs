using System.Text.Json.Serialization;

namespace Models.CreateRequests;

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

/// <summary>
/// Record class representing a request to create a Transaction.
/// </summary>
public record CreateTransactionRequest : CreateRequestBase, ICreateTransactionRequest
{
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