using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing an Account Mapping.
/// Account Mappings are children of a Month and an Account, and are related to a Budget, Incomes, 
/// Transactions and Transaction Applications.
/// </summary>
public class AccountMapping
{
    #region Properties

    /// <inheritdoc cref="IAccountMapping.Id"/>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Account
    /// </summary>
    public Account Account { get; } = null!;

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <inheritdoc cref="IAccountMapping.StartingBalance"/>
    public decimal StartingBalance { get; }

    /// <summary>
    /// Navigation to the related Budget
    /// </summary>
    public Budget? Budget { get; } = null!;

    /// <inheritdoc cref="IAccountMapping.BudgetType"/>
    public BudgetType? BudgetType { get; }

    /// <inheritdoc cref="IAccountMapping.IsDefault"/>
    public bool IsDefault { get; }

    /// <summary>
    /// Navigation to the related Override Incomes
    /// </summary>
    /// <remark>
    /// This navigation only maps to Incomes that specify this Account Mapping as their override mapping. This
    /// is the case when an Income amount should be credited to an account other than the default spend account.
    /// </remark>
    public ICollection<Income> OverrideIncomes { get; } = null!;

    /// <summary>
    /// Navigation to the related Credit Card Transactions.
    /// </summary>
    /// <remark>
    /// If this is a credit card Account Mapping, Transactions that were placed on credit cards will specify this
    /// account mapping to indicate the transaction amount was placed on this credit card.
    /// This collection should always be empty if this is a non-credit card Account Mapping.
    /// </remark>
    public ICollection<Transaction> CreditCardTransactions { get; } = null!;

    /// <summary>
    /// Navigation to the related Override Transaction Applications
    /// </summary>
    /// <remarks>
    /// This navigation only maps to Transaction Applications that specify this Account Mapping as their override
    /// mapping. This collection should only have Transaction Applications if IsDefault is false.
    /// If IsDefault is true, this collection should always be empty since an override isn't needed to 
    /// use this mapping. 
    /// </remarks>
    public ICollection<TransactionApplication> OverrideTransactionApplications { get; } = null!;

    #endregion

    #region Methods

    #endregion
}