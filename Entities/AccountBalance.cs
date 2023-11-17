namespace Entities;

/// <summary>
/// Entity class representing an Account Balance.
/// Account Balances are children of an Account and a Month.
/// </summary>
public class AccountBalance
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Account
    /// </summary>
    public Account Account { get; } = null!;

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <summary>
    /// Date of the Account Balance
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// Amount of the Account Balance
    /// </summary>
    public decimal Amount { get; }

    #endregion

    #region Methods

    #endregion
}
