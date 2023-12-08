using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing a Budget.
/// Budgets are children of a Month, and related to an Account Mapping and Transaction Applications.
/// </summary>
public class Budget
{
    #region Properties

    /// <inheritdoc cref="IBudget.Id"/>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <inheritdoc cref="IBudget.Name"/>
    public string Name { get; } = null!;

    /// <inheritdoc cref="IBudget.Type"/>
    public BudgetType Type { get; }

    /// <inheritdoc cref="IBudget.Amount"/>
    public decimal Amount { get; }

    /// <inheritdoc cref="IBudget.RolloverAmount"/>
    public decimal? RolloverAmount { get; }

    /// <inheritdoc cref="IBudget.IsRolloverAmountOverridden"/>
    public bool? IsRolloverAmountOverridden { get; }

    /// <summary>
    /// Navigation to the related Account Mapping
    /// </summary>
    /// <remarks>
    /// If not null, there's an Account Mapping that maps directly to this individual budget.
    /// If null, there's an Account Mapping that maps to this budget's type.
    /// </remarks>
    public AccountMapping? AccountMapping { get; } = null!;

    /// <summary>
    /// Navigation to the related Transaction Applications
    /// </summary>
    public ICollection<TransactionApplication> TransactionApplications { get; } = null!;

    #endregion

    #region Methods

    #endregion
}