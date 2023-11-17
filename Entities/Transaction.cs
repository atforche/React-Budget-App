namespace Entities;

/// <summary>
/// Entity class representing a Transaction.
/// Transactions are children of a Month, parents of Transaction Applications, and related to an Account Mapping.
/// </summary>
public class Transaction
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

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
    public TransactionType Type { get; }

    /// <summary>
    /// Amount of the Transaction
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Navigation to the related Credit Card Account Mapping
    /// </summary>
    /// <remark>
    /// If not null, this Transaction was made on a credit card. Any Applications under this Transaction should
    /// count against their Budgets but not decrement their mapped Accounts until this Transaction is paid off.
    /// </remark>
    public AccountMapping? CreditCardAccountMapping { get; } = null!;

    /// <summary>
    /// Paid Off Date of the Transaction
    /// </summary>
    /// <remarks>
    /// For a credit card Transaction, this date indicates when the credit card balance for this transaction was 
    /// paid off.
    /// </remarks>
    public DateTime? PaidOffDate { get; } = null!;

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
    Credit,

    /// <summary>
    /// A Transfer Transaction represents money that is debited from one budget and credited to another
    /// </summary>
    Transfer
}
