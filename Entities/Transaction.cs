namespace Entities;

/// <summary>
/// Entity class representing a Transaction.
/// Transactions are children of a Month and parents of a Transaction Application.
/// </summary>
public class Transaction
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Date of the Transaction
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// Location of the Transaction
    /// </summary>
    public string Location { get; } = null!;

    /// <summary>
    /// Description of the Transaction
    /// </summary>
    public string Description { get; } = null!;

    /// <summary>
    /// Type of the Transaction
    /// </summary>
    public TransactionType TransactionType { get; }

    /// <summary>
    /// Amount of the Transaction
    /// </summary>
    public decimal Amount { get; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <summary>
    /// Navigation to the child Transaction Applications
    /// </summary>
    public ICollection<TransactionApplication> TransactionApplications { get; } = null!;

    #endregion

    #region Methods

    #endregion
}

/// <summary>
/// Enum representing the different types of Transactions
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// A Debit Transaction represents money that was spent against a budget
    /// </summary>
    Debit,

    /// <summary>
    /// A Credit Transaction represents money that was saved / put back towards a budget
    /// </summary>
    Credit
}
