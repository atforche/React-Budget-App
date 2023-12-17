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
    public required long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public required Month Month { get; set; }

    /// <summary>
    /// Navigation to the parent Employer
    /// </summary>
    public Employer? Employer { get; set; }

    /// <inheritdoc cref="IIncome.Date"/>
    public required DateTime Date { get; set; }

    /// <Inheritdoc cref="IIncome.Amount"/>
    public required decimal Amount { get; set; }

    /// <inheritdoc cref="IIncome.Description"/>
    public required string Description { get; set; }

    /// <summary>
    /// Navigation to the related Override Account Mapping
    /// </summary>
    public AccountMapping? OverrideAccountMapping { get; set; }

    /// <summary>
    /// Navigation to the child Employer Income Information
    /// </summary>
    public EmployerIncomeInformation? EmployerIncomeInformation { get; set; }

    #endregion

    #region Methods

    #endregion
}
