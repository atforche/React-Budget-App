using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing an Employer Income Rate.
/// Employer Income Rates are children of a Month and an Employer, and parents of an Employer Income Information.
/// </summary>
public class EmployerIncomeRate
{
    #region Properties

    /// <inheritdoc cref="IEmployerIncomeRate.Id"/>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Employer
    /// </summary>
    public Employer Employer { get; } = null!;

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <summary>
    /// Navigation to the child Employer Income Information
    /// </summary>
    public EmployerIncomeInformation EmployerIncomeInformation { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
