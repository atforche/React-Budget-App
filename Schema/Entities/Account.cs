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
    public required long Id { get; set; }

    /// <inheritdoc cref="IAccount.Name"/>
    public required string Name { get; set; }

    /// <inheritdoc cref="IAccount.Type"/>
    public required AccountType Type { get; set; }

    /// <summary>
    /// Navigation to the child Account Balances
    /// </summary>
    public required ICollection<AccountBalance> AccountBalances { get; set; }

    /// <summary>
    /// Navigation to the child Account Mappings
    /// </summary>
    public required ICollection<AccountMapping> AccountMappings { get; set; }

    #endregion

    #region Methods

    #endregion
}


