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
    public required long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public required Month Month { get; set; }

    /// <inheritdoc cref="IBudget.Name"/>
    public required string Name { get; set; }

    /// <inheritdoc cref="IBudget.Type"/>
    public required BudgetType Type { get; set; }

    /// <inheritdoc cref="IBudget.Amount"/>
    public required decimal Amount { get; set; }

    /// <inheritdoc cref="IBudget.RolloverAmount"/>
    public decimal? RolloverAmount { get; set; }

    /// <inheritdoc cref="IBudget.IsRolloverAmountOverridden"/>
    public bool? IsRolloverAmountOverridden { get; set; }

    /// <summary>
    /// Navigation to the related Account Mapping
    /// </summary>
    /// <remarks>
    /// If not null, there's an Account Mapping that maps directly to this individual budget.
    /// If null, there's an Account Mapping that maps to this budget's type.
    /// </remarks>
    public AccountMapping? AccountMapping { get; set; }

    /// <summary>
    /// Navigation to the related Transaction Applications
    /// </summary>
    public required ICollection<TransactionApplication> TransactionApplications { get; set; }

    #endregion

    #region Methods

    #endregion
}