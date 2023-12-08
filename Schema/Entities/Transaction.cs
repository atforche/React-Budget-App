using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing a Transaction.
/// Transactions are children of a Month, parents of Transaction Applications, and related to an Account Mapping.
/// </summary>
public class Transaction
{
    #region Properties

    /// <inheritdoc cref="ITransaction.Id"/>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <inheritdoc cref="ITransaction.Date"/>
    public DateTime Date { get; }

    /// <inheritdoc cref="ITransaction.Location"/>
    public string Location { get; } = null!;

    /// <inheritdoc cref="ITransaction.Type"/>
    public TransactionType Type { get; }

    /// <inheritdoc cref="ITransaction.Amount"/>
    public decimal Amount { get; }

    /// <summary>
    /// Navigation to the related Credit Card Account
    /// </summary>
    public Account? CreditCardAccount { get; } = null!;

    /// <inheritdoc cref="ITransaction.CreditCardAccountId"/>
    public DateTime? PaidOffDate { get; } = null!;

    /// <summary>
    /// Navigation to the child Transaction Applications
    /// </summary>
    public ICollection<TransactionApplication> TransactionApplications { get; } = null!;

    #endregion

    #region Methods

    #endregion
}