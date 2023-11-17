namespace Entities;

/// <summary>
/// Entity class representing an Income.
/// Incomes are children of a Month and related to an Account Mapping. They are also sometimes children of an Employer and 
/// parents of an Employer Income Information.
/// </summary>
public class Income
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <summary>
    /// Navigation to the parent Employer
    /// </summary>
    public Employer? Employer { get; } = null!;

    /// <summary>
    /// Date for the Income
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// Amount for the Income
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Description for the Income
    /// </summary>
    public string Description { get; } = null!;

    /// <summary>
    /// Navigation to the related Override Account Mapping
    /// </summary>
    /// If null, this Income will be credited to the Account mapped to the Spend budget type
    /// If not null, this Income will be credited the Account specified on this Account Mapping.
    public AccountMapping? OverrideAccountMapping { get; } = null!;

    /// <summary>
    /// Navigation to the child Employer Income Information
    /// </summary>
    public EmployerIncomeInformation? EmployerIncomeInformation { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
