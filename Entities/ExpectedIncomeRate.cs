namespace Entities;

/// <summary>
/// Entity class representing an Expected Income Rate.
/// Expected Income Rates are children of a Month and an Employer, and parents of Income Information.
/// </summary>
public class ExpectedIncomeRate
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Employer
    /// </summary>
    public Employer Employer { get; } = null!;

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <summary>
    /// Navigation to the child Income Information
    /// </summary>
    public IncomeInformation IncomeInformation { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
