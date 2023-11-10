namespace Entities;

/// <summary>
/// Entity class representing a Transaction Application.
/// Transactions Applications are children of Transactions, Budgets, and Account Mappings.
/// </summary>
public class TransactionApplication
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Amount of the Transaction Application
    /// </summary>
    public decimal Amount { get; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Transaction
    /// </summary>
    public Transaction Transaction { get; } = null!;

    /// <summary>
    /// Navigation to the parent Budget
    /// </summary>
    public Budget Budget { get; } = null!;

    /// <summary>
    /// Navigation to the parent Account Mapping Override
    /// </summary>
    /// <remarks>
    /// If not null, this Transaction Application is applied against a non-primary Account Mapping for this Budget.
    /// This should only be populated if the Budget or its Budget Type is mapped to by multiple Account Mappings. 
    /// </remarks>
    public AccountMapping? AccountMappingOverride { get; } = null!;

    #endregion

    #region Methods

    #endregion
}