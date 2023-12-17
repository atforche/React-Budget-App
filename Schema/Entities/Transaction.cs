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
    public required long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public required Month Month { get; set; }

    /// <inheritdoc cref="ITransaction.Date"/>
    public required DateTime Date { get; set; }

    /// <inheritdoc cref="ITransaction.Location"/>
    public required string Location { get; set; }

    /// <inheritdoc cref="ITransaction.Type"/>
    public required TransactionType Type { get; set; }

    /// <inheritdoc cref="ITransaction.Amount"/>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Navigation to the related Credit Card Account
    /// </summary>
    public Account? CreditCardAccount { get; set; }

    /// <inheritdoc cref="ITransaction.CreditCardAccountId"/>
    public DateTime? PaidOffDate { get; set; }

    /// <summary>
    /// Navigation to the child Transaction Applications
    /// </summary>
    public required ICollection<TransactionApplication> TransactionApplications { get; set; }

    #endregion

    #region Methods

    #endregion
}