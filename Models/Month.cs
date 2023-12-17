namespace Models;

/// <summary>
/// Read-only interface representing a Month.
/// </summary>
public interface IMonth
{
    /// <summary>
    /// Id of the Month
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Year of the Month
    /// </summary>
    int Year { get; }

    /// <summary>
    /// Number representing the Month (1-12)
    /// </summary>
    int MonthNumber { get; }

    /// <summary>
    /// Child Budgets
    /// </summary>
    ICollection<IBudget> Budgets { get; }

    /// <summary>
    /// Child Account Mappings
    /// </summary>
    ICollection<IAccountMapping> AccountMappings { get; }

    /// <summary>
    /// Child Transactions
    /// </summary>
    ICollection<ITransaction> Transactions { get; }

    /// <summary>
    /// Child Employer Income Rates
    /// </summary>
    ICollection<IEmployerIncomeRate> EmployerIncomeRates { get; }

    /// <summary>
    /// Child Incomes
    /// </summary>
    ICollection<IIncome> Incomes { get; }

    /// <summary>
    /// Child Account Balances
    /// </summary>
    ICollection<IAccountBalance> AccountBalances { get; }
}

/// <summary>
/// Record class representing a Month.
/// </summary>
public record Month : IMonth
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required int Year { get; init; }

    /// <inheritdoc/>
    public required int MonthNumber { get; init; }

    /// <inheritdoc/>
    public required ICollection<IBudget> Budgets { get; init; }

    /// <inheritdoc/>
    public required ICollection<IAccountMapping> AccountMappings { get; init; }

    /// <inheritdoc/>
    public required ICollection<ITransaction> Transactions { get; init; }

    /// <inheritdoc/>
    public required ICollection<IEmployerIncomeRate> EmployerIncomeRates { get; init; }

    /// <inheritdoc/>
    public required ICollection<IIncome> Incomes { get; init; }

    /// <inheritdoc/>
    public required ICollection<IAccountBalance> AccountBalances { get; init; }
}