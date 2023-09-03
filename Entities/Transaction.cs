using System;

/// <summary>
/// Entity class representing a Transaction.
/// Transactions are children of a Budget.
/// </summary>
public class Transaction
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long TransactionId { get; set; }

    /// <summary>
    /// Date of the transaction
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Location of the transaction
    /// </summary>
    public string Location { get; set; } = null!;

    /// <summary>
    /// Description of the transaction
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Type of the transaction
    /// </summary>
    public TransactionType TransactionType { get; set; }

    /// <summary>
    /// Amount of the transaction
    /// </summary>
    public decimal Amount { get; set; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Budget
    /// </summary>
    public Budget Budget { get; } = null!;

    #endregion

    #region Methods

    #endregion
}

/// <summary>
/// Enum representing the different types of transactions
/// </summary>
public enum TransactionType
{
    Debit,
    Credit
}
