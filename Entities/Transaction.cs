namespace Entities;

/// <summary>
/// Entity class representing a Transaction.
/// Transactions are children of a Budget.
/// </summary>
[ExcelTable(TableName = "Transactions")]
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
    [ExcelColumn(ColumnName = "Date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Location of the transaction
    /// </summary>
    [ExcelColumn(ColumnName = "Location")]
    public string Location { get; set; } = null!;

    /// <summary>
    /// Description of the transaction
    /// </summary>
    [ExcelColumn(ColumnName = "Description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Type of the transaction
    /// </summary>
    [ExcelColumn(ColumnName = "Type")]
    public TransactionType TransactionType { get; set; }

    /// <summary>
    /// Amount of the transaction
    /// </summary>
    [ExcelColumn(ColumnName = "Amount")]
    public decimal Amount { get; set; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Budget
    /// </summary>
    [ExcelColumn(ColumnName = "Category")]
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
