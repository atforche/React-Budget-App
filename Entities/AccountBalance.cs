using System;

namespace Entities;

/// <summary>
/// Entity class representing an Account Balance.
/// Account Balances are children of a Month.
/// </summary>
public class AccountBalance
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long AccountBalanceId { get; set; }

    /// <summary>
    /// Date of the account balance
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Account Name of the account balance
    /// </summary>
    public string AccountName { get; set; } = null!;

    /// <summary>
    /// Account Type of the account balance
    /// </summary>
    public AccountType AccountType { get; set; }

    /// <summary>
    /// Amount of the account balance
    /// </summary>
    public decimal Amount { get; set; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    #endregion

    #region Methods

    #endregion
}

public enum AccountType
{
    Spending,
    Reserve,
    SafetyNet,
    Savings,
    Retirement,
    Loan
}
