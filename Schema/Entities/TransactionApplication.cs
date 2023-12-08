using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing a Transaction Application.
/// Transactions Applications are children of a Transaction, and are related to a Budget and an Account Mapping.
/// </summary>
public class TransactionApplication
{
    #region Properties

    /// <interface cref="ITransactionApplication.Id"/>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Transaction
    /// </summary>
    public Transaction Transaction { get; } = null!;

    /// <inheritdoc cref="ITransactionApplication.Type"/>
    public ApplicationType Type { get; set; }

    /// <inheritdoc cref="ITransactionApplication.Description"/>
    public string Description { get; } = null!;

    /// <inheritdoc cref="ITransactionApplication.Amount"/>
    public decimal Amount { get; }

    /// <summary>
    /// Navigation to the related Budget
    /// </summary>
    public Budget Budget { get; } = null!;

    /// <summary>
    /// Navigation to the related Override Account Mapping
    /// </summary>
    public AccountMapping? OverrideAccountMapping { get; } = null!;

    #endregion

    #region Methods

    #endregion
}