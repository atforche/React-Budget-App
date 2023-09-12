namespace Entities;

/// <summary>
/// Abstract Entity class representing Income.
/// Incomes are children of a Month.
/// </summary>
public abstract class Income
{
    #region Properties

    /// <summary>
    /// Gross Amount of the income
    /// </summary>
    [ExcelColumn(ColumnName = "Gross Income")]
    public decimal GrossAmount { get; set; }

    /// <summary>
    /// Healthcare Deductions of the income
    /// </summary>
    [ExcelColumn(ColumnName = "Healthcare Deductions")]
    public decimal HealthcareDeductions { get; set; }

    /// <summary>
    /// Other Deductions of the income
    /// </summary>
    [ExcelColumn(ColumnName = "Other Payroll Deductions")]
    public decimal OtherDeductions { get; set; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Employer
    /// </summary>
    public Employer Employer { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
