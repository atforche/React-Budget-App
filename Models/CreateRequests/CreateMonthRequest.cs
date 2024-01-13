namespace Models.CreateRequests;

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
    int Year { get; init; }

    /// <inheritdoc cref="IMonth.MonthNumber"/>
    int MonthNumber { get; init; }

    /// <inheritdoc cref="IMonth.Budgets"/>
    ICollection<ICreateBudgetRequest> Budgets { get; init; }

    /// <inheritdoc cref="IMonth.AccountMappings"/>
    ICollection<ICreateAccountMappingRequest> AccountMappings { get; init; }

    /// <inheritdoc cref="IMonth.Transactions"/>
    ICollection<ICreateTransactionRequest> Transactions { get; init; }

    /// <inheritdoc cref="IMonth.Incomes"/>
    ICollection<ICreateIncomeRequest> Incomes { get; init; }

    /// <inheritdoc cref="IMonth.EmployerIncomeRates"/>
    ICollection<ICreateEmployerIncomeRateRequest> EmployerIncomeRates { get; init; }

    /// <inheritdoc cref="IMonth.AccountBalances"/>
    ICollection<ICreateAccountBalanceRequest> AccountBalances { get; init; }
}

/// <summary>
/// Record class representing a request to create a Month.
/// </summary>
public record CreateMonthRequest : CreateRequestBase, ICreateMonthRequest
{
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