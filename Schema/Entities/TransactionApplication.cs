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
    public required long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Transaction
    /// </summary>
    public required Transaction Transaction { get; set; }

    /// <inheritdoc cref="ITransactionApplication.Type"/>
    public required ApplicationType Type { get; set; }

    /// <inheritdoc cref="ITransactionApplication.Description"/>
    public required string Description { get; set; }

    /// <inheritdoc cref="ITransactionApplication.Amount"/>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Navigation to the related Budget
    /// </summary>
    public required Budget Budget { get; set; }

    /// <summary>
    /// Navigation to the related Override Account Mapping
    /// </summary>
    public AccountMapping? OverrideAccountMapping { get; set; }

    #endregion

    #region Methods

    #endregion
}