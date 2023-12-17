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
    public required long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Account
    /// </summary>
    public required Account Account { get; set; }

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public required Month Month { get; set; }

    /// <inheritdoc cref="IAccountBalance.Date"/>
    public required DateTime Date { get; set; }

    /// <inheritdoc cref="IAccountBalance.Amount"/>
    public required decimal Amount { get; set; }

    #endregion

    #region Methods

    #endregion
}
