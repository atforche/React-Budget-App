using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing an Employer.
/// Employers are parents to Employer Income Rates and Incomes.
/// </summary>
public class Employer
{
    #region Properties

    /// <inheritdoc cref="IEmployer.Id"/>
    public required long Id { get; set; }

    /// <inheritdoc cref="IEmployer.Name"/>
    public required string Name { get; set; }

    /// <summary>
    /// Navigation to the child Employer Income Rates
    /// </summary>
    public required ICollection<EmployerIncomeRate> EmployerIncomeRates { get; set; }

    /// <summary>
    /// Navigation to the related Incomes
    /// </summary>
    public required ICollection<Income> Incomes { get; set; }

    #endregion

    #region Methods

    #endregion
}
