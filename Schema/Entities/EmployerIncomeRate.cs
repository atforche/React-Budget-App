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
    public required long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Employer
    /// </summary>
    public required Employer Employer { get; set; }

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public required Month Month { get; set; }

    /// <summary>
    /// Navigation to the child Employer Income Information
    /// </summary>
    public required EmployerIncomeInformation EmployerIncomeInformation { get; set; }

    #endregion

    #region Methods

    #endregion
}
