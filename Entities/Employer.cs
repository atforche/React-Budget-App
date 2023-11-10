namespace Entities;

/// <summary>
/// Entity class representing an Employer.
/// Employers are parents to Expected Income Rates and Income Dates.
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

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the child Expected Income Rates
    /// </summary>
    public ICollection<ExpectedIncomeRate> ExpectedIncomeRate { get; } = null!;

    /// <summary>
    /// Navigation to the child Income Dates
    /// </summary>
    public ICollection<IncomeDate> IncomeDates { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
