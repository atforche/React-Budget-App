using System.Text.Json.Serialization;

namespace Models.ChangeRequests;

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
/// Record class representing a request to change a Transaction.
/// </summary>
public record ChangeTransactionRequest : IChangeTransactionRequest
{
    /// <inheritdoc/>
    public required long Id { get; init; }

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
    public required ICollection<IChangeTransactionApplicationRequest> ChangeTransactionApplicationRequests { get; init; }
}