using System;

namespace Entities;

/// <summary>
/// Abstract Entity class representing Income.
/// Incomes are children of a Month.
/// </summary>
public abstract class Income
{
    #region Properties

    /// <summary>
    /// Date of the income
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Employer Name of the income
    /// </summary>
    public string Employer { get; set; } = null!;

    /// <summary>
    /// Gross Amount of the income
    /// </summary>
    public decimal GrossAmount { get; set; }

    /// <summary>
    /// Healthcare Deductions of the income
    /// </summary>
    public decimal HealthcareDeductions { get; set; }

    /// <summary>
    /// Other Deductions of the income
    /// </summary>
    public decimal OtherDeductions { get; set; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
