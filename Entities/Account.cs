namespace Entities;

/// <summary>
/// Entity class representing an Account.
/// Accounts are parents of Account Balances and Account Mappings.
/// </summary>
public class Account
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Name of this Account
    /// </summary>
    public string Name { get; } = null!;

    /// <summary>
    /// Account Type of this Account
    /// </summary>
    public AccountType AccountType { get; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the child Account Balances
    /// </summary>
    public ICollection<AccountBalance> AccountBalances { get; } = null!;

    /// <summary>
    /// Navigation to the child Account Mappings
    /// </summary>
    public ICollection<AccountMapping> AccountMappings { get; } = null!;

    #endregion

    #region Methods

    #endregion
}

/// <summary>
/// Enum representing the different types of Accounts
/// </summary>
public enum AccountType
{
    /// <summary>
    /// Regular accounts represent any account that is mapped to a particular Budget or Budget Type. The expected balance
    /// of these accounts can be determined using the starting balance of the Account Mapping and any Transactions against
    /// the mapped Budget or Budget Type.
    /// </summary>
    Regular,

    /// <summary>
    /// Credit Card accounts represent any credit card account. These accounts serve as a spending buffer, so any
    /// balance that they carry offsets debits from the Spending account. Balances for these accounts must be
    /// manually entered every week.
    /// </summary>
    CreditCard,

    /// <summary>
    /// Retirement accounts represent any 401(k), 403(b), 457, or pension accounts. These accounts grow with pre-tax
    /// contributions, employer contributions, and changes in the stock market. Balances for these accounts must be
    /// manually entered every week.
    /// </summary>
    Retirement,

    /// <summary>
    /// Loan accounts represent any loan or mortgage accounts. These accounts decrease with regular payments and 
    /// increase through accrued interest. Balances for these accounts must be manually entered every week.
    /// </summary>
    Loan
}
