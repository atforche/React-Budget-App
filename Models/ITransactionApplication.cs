using System.Text.Json.Serialization;

namespace Models;

/// <summary>
/// Read-only interface representing a Transaction Application.
/// </summary>
[ExcelTable("Transactions.{Month}")]
public interface ITransactionApplication
{
    /// <summary>
    /// Id of the Transaction Application
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Id of the parent Transaction
    /// </summary>
    long TransactionId { get; }

    /// <summary>
    /// Type of the Transaction Application
    /// </summary>
    [ExcelColumn("Type", typeof(string))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    ApplicationType Type { get; }

    /// <summary>
    /// Description of the Transaction Application
    /// </summary>
    [ExcelColumn("Description", typeof(string), true)]
    string Description { get; }

    /// <summary>
    /// Amount of the Transaction Application
    /// </summary>
    [ExcelColumn("Amount", typeof(decimal))]
    decimal Amount { get; }

    /// <summary>
    /// Id of the related Budget
    /// </summary>
    [ExcelColumn("Budget", typeof(string), true)]
    long BudgetId { get; }

    /// <summary>
    /// Id of the related Override Account Mapping
    /// </summary>
    /// <remarks>
    /// If not null, this Transaction Application is applied against a non-primary Account mapped to this Budget.
    /// This should only be populated if the Budget or its Budget Type is mapped to by multiple Accounts. 
    /// </remarks>
    [ExcelColumn("Override Account Mapping", typeof(string), true)]
    long? OverrideAccountMappingId { get; }
}

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
    long? TransactionId { get; }

    /// <inheritdoc cref="ITransactionApplication.Type"/>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    ApplicationType Type { get; }

    /// <inheritdoc cref="ITransactionApplication.Description"/>
    string Description { get; }

    /// <inheritdoc cref="ITransactionApplication.Amount"/>
    decimal Amount { get; }

    /// <inheritdoc cref="ITransactionApplication.BudgetId"/>
    long BudgetId { get; }

    /// <summary>
    /// The related <see cref="IAccount.Id"/> or <see cref="ICreateAccountRequest.Id"/>
    /// </summary>
    long? OverrideAccountMappingId { get; }
}

public record CreateTransactionApplicationRequest : ICreateTransactionApplicationRequest
{
    private static long Sequence = long.MaxValue;

    /// <inheritdoc/>
    public long Id { get; } = Sequence--;

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

/// <summary>
/// Interface representing a request to change a Transaction Application.
/// </summary>
public interface IChangeTransactionApplicationRequest
{
    /// <inheritdoc cref="ITransactionApplication.Id"/>
    long Id { get; }

    /// <inheritdoc cref="ITransactionApplication.Type"/>
    [JsonConverter(typeof(JsonStringEnumConverter))]
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
/// Enum representing the different types of Applications
/// </summary>
public enum ApplicationType
{
    /// <summary>
    /// A Debit Application represents an amount that should be debited from a budget
    /// </summary>
    Debit,

    /// <summary>
    /// A Credit Application represents an amount that should be credited to a budget
    /// </summary>
    Credit
}