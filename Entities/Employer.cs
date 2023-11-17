namespace Entities;

/// <summary>
/// Entity class representing an Employer.
/// Employers are parents to Employer Income Rates and Incomes.
/// </summary>
public class Employer
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Name of the Employer
    /// </summary>
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
