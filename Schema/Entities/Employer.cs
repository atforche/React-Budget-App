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
    public long Id { get; set; }

    /// <inheritdoc cref="IEmployer.Name"/>
    public string Name { get; } = null!;

    /// <summary>
    /// Navigation to the child Employer Income Rates
    /// </summary>
    public ICollection<EmployerIncomeRate> EmployerIncomeRates { get; } = null!;

    /// <summary>
    /// Navigation to the related Incomes
    /// </summary>
    public ICollection<Income> Incomes { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
