using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing a Month.
/// A Month is the parent entity of any data that occurs during a given month.
/// </summary>
public class Month
{
    #region Properties

    /// <inheritdoc cref="IMonth.Id"/>
    public long Id { get; set; }

    /// <inheritdoc cref="IMonth.Year"/>
    public int Year { get; }

    /// <inheritdoc cref="IMonth.MonthNumber"/>
    public int MonthNumber { get; }

    /// <summary>
    /// Navigation to the child Budgets
    /// </summary>
    public ICollection<Budget> Budgets { get; } = null!;

    /// <summary>
    /// Navigation to the child Account Mappings
    /// </summary>
    public ICollection<AccountMapping> AccountMappings { get; } = null!;

    /// <summary>
    /// Navigation to the child Transactions
    /// </summary>
    public ICollection<Transaction> Transactions { get; } = null!;

    /// <summary>
    /// Navigation to the child Employer Income Rates
    /// </summary>
    public ICollection<EmployerIncomeRate> EmployerIncomeRates { get; } = null!;

    /// <summary>
    /// Navigation to the child Incomes
    /// </summary>
    public ICollection<Income> Incomes { get; } = null!;

    /// <summary>
    /// Navigation to the child Account Balances
    /// </summary>
    public ICollection<AccountBalance> AccountBalances { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
