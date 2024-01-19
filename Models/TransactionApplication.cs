using Models.Attributes;

namespace Models;

/// <summary>
/// Read-only interface representing a Transaction Application.
/// </summary>
[ExcelTable("Transactions.{Month}", parentType: typeof(ITransaction))]
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
/// Record class representing a Transaction Application.
/// </summary>
public record TransactionApplication : ITransactionApplication
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required long TransactionId { get; init; }

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
/// Enum representing the different types of Applications.
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