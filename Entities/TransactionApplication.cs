namespace Entities;

/// <summary>
/// Entity class representing a Transaction Application.
/// Transactions Applications are children of a Transaction, and are related to a Budget and an Account Mapping.
/// </summary>
public class TransactionApplication
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Transaction
    /// </summary>
    public Transaction Transaction { get; } = null!;

    /// <summary>
    /// Type of the Transaction Application
    /// </summary>
    public ApplicationType Type { get; set; }

    /// <summary>
    /// Amount of the Transaction Application
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Navigation to the related Budget
    /// </summary>
    public Budget Budget { get; } = null!;

    /// <summary>
    /// Navigation to the related Override Account Mapping
    /// </summary>
    /// <remarks>
    /// If not null, this Transaction Application is applied against a non-primary Account mapped to this Budget.
    /// This should only be populated if the Budget or its Budget Type is mapped to by multiple Accounts. 
    /// </remarks>
    public AccountMapping? OverrideAccountMapping { get; } = null!;

    #endregion

    #region Methods

    #endregion
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