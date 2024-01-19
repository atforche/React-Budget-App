namespace Models.CreateRequests;

/// <summary>
/// Interface representing a request to create a Transaction Application.
/// </summary>
public interface ICreateTransactionApplicationRequest
{
    /// <summary>
    /// Id of the Create Transaction Application Request
    /// </summary>
    long Id { get; }

    /// <summary>
    /// The parent <see cref="ITransaction.Id"/> or <see cref="ICreateTransactionRequest.Id"/>
    /// If null, this request must fall under a <see cref="ICreateTransactionRequest"/> and the value will be inferred.
    /// </summary>
    long? TransactionId { get; init; }

    /// <inheritdoc cref="ITransactionApplication.Type"/>
    ApplicationType Type { get; init; }

    /// <inheritdoc cref="ITransactionApplication.Description"/>
    string Description { get; init; }

    /// <inheritdoc cref="ITransactionApplication.Amount"/>
    decimal Amount { get; init; }

    /// <inheritdoc cref="ITransactionApplication.BudgetId"/>
    long BudgetId { get; init; }

    /// <summary>
    /// The related <see cref="IAccount.Id"/> or <see cref="ICreateAccountRequest.Id"/>
    /// </summary>
    long? OverrideAccountMappingId { get; init; }
}

/// <summary>
/// Record class representing a request to create a Transaction Application.
/// </summary>
public record CreateTransactionApplicationRequest : CreateRequestBase, ICreateTransactionApplicationRequest
{
    /// <inheritdoc/>
    public long? TransactionId { get; init; }

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