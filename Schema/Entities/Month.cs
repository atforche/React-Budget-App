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
    public required long Id { get; set; }

    /// <inheritdoc cref="IMonth.Year"/>
    public required int Year { get; set; }

    /// <inheritdoc cref="IMonth.MonthNumber"/>
    public required int MonthNumber { get; set; }

    /// <summary>
    /// Navigation to the child Budgets
    /// </summary>
    public required ICollection<Budget> Budgets { get; set; }

    /// <summary>
    /// Navigation to the child Account Mappings
    /// </summary>
    public required ICollection<AccountMapping> AccountMappings { get; set; }

    /// <summary>
    /// Navigation to the child Transactions
    /// </summary>
    public required ICollection<Transaction> Transactions { get; set; }

    /// <summary>
    /// Navigation to the child Employer Income Rates
    /// </summary>
    public required ICollection<EmployerIncomeRate> EmployerIncomeRates { get; set; }

    /// <summary>
    /// Navigation to the child Incomes
    /// </summary>
    public required ICollection<Income> Incomes { get; set; }

    /// <summary>
    /// Navigation to the child Account Balances
    /// </summary>
    public required ICollection<AccountBalance> AccountBalances { get; set; }

    #endregion

    #region Methods

    #endregion
}
