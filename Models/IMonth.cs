using System.Reflection.Emit;

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
/// Interface representing a request to create a Month.
/// </summary>
public interface ICreateMonthRequest
{
    /// <summary>
    /// Id of the Create Month Request
    /// </summary>
    long Id { get; }

    /// <inheritdoc cref="IMonth.Year"/>
    int Year { get; }

    /// <inheritdoc cref="IMonth.MonthNumber"/>
    int MonthNumber { get; }

    /// <inheritdoc cref="IMonth.Budgets"/>
    ICollection<ICreateBudgetRequest> Budgets { get; }

    /// <inheritdoc cref="IMonth.AccountMappings"/>
    ICollection<ICreateAccountMappingRequest> AccountMappings { get; }

    /// <inheritdoc cref="IMonth.Transactions"/>
    ICollection<ICreateTransactionRequest> Transactions { get; }

    /// <inheritdoc cref="IMonth.Incomes"/>
    ICollection<ICreateIncomeRequest> Incomes { get; }

    /// <inheritdoc cref="IMonth.EmployerIncomeRates"/>
    ICollection<ICreateEmployerIncomeRateRequest> EmployerIncomeRates { get; }

    /// <inheritdoc cref="IMonth.AccountBalances"/>
    ICollection<ICreateAccountBalanceRequest> AccountBalances { get; }
}

public record CreateMonthRequest : ICreateMonthRequest
{
    private static long Sequence = 1;

    /// <inheritdoc/>
    public long Id { get; } = Sequence++;

    /// <inheritdoc/>
    public required int Year { get; init; }

    /// <inheritdoc/>
    public required int MonthNumber { get; init; }

    /// <inheritdoc/> 
    public required ICollection<ICreateBudgetRequest> Budgets { get; init; }

    /// <inheritdoc/>
    public required ICollection<ICreateAccountMappingRequest> AccountMappings { get; init; }

    /// <inheritdoc/>
    public required ICollection<ICreateTransactionRequest> Transactions { get; init; }

    /// <inheritdoc/>
    public required ICollection<ICreateIncomeRequest> Incomes { get; init; }

    /// <inheritdoc/>
    public required ICollection<ICreateEmployerIncomeRateRequest> EmployerIncomeRates { get; init; }

    /// <inheritdoc/>
    public required ICollection<ICreateAccountBalanceRequest> AccountBalances { get; init; }
}