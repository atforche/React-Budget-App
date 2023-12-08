using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing an Income.
/// Incomes are children of a Month and related to an Account Mapping. They are also sometimes children of an Employer and 
/// parents of an Employer Income Information.
/// </summary>
public class Income
{
    #region Properties

    /// <inheritdoc cref="IIncome.Id"/>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <summary>
    /// Navigation to the parent Employer
    /// </summary>
    public Employer? Employer { get; } = null!;

    /// <inheritdoc cref="IIncome.Date"/>
    public DateTime Date { get; }

    /// <Inheritdoc cref="IIncome.Amount"/>
    public decimal Amount { get; }

    /// <inheritdoc cref="IIncome.Description"/>
    public string Description { get; } = null!;

    /// <summary>
    /// Navigation to the related Override Account Mapping
    /// </summary>
    public AccountMapping? OverrideAccountMapping { get; } = null!;

    /// <summary>
    /// Navigation to the child Employer Income Information
    /// </summary>
    public EmployerIncomeInformation? EmployerIncomeInformation { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
