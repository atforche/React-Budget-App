using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing an Account Balance.
/// Account Balances are children of an Account and a Month.
/// </summary>
public class AccountBalance
{
    #region Properties

    /// <inheritdoc cref="IAccountBalance.Id"/>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Account
    /// </summary>
    public Account Account { get; } = null!;

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <inheritdoc cref="IAccountBalance.Date"/>
    public DateTime Date { get; }

    /// <inheritdoc cref="IAccountBalance.Amount"/>
    public decimal Amount { get; }

    #endregion

    #region Methods

    #endregion
}
