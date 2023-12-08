using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing an Account.
/// Accounts are parents of Account Balances and Account Mappings.
/// </summary>
public class Account
{
    #region Properties

    /// <inheritdoc cref="IAccount.Id"/>
    public long Id { get; set; }

    /// <inheritdoc cref="IAccount.Name"/>
    public string Name { get; } = null!;

    /// <inheritdoc cref="IAccount.Type"/>
    public AccountType Type { get; }

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


