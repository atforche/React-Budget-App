using System;

namespace Entities;

/// <summary>
/// Entity class representing an Expected Income Date.
/// Expected Income Dates are children of an Expected Income Rate.
/// </summary>
[ExcelTable(TableName = "Expected.Income.Dates")]
public class ExpectedIncomeDate
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long ExpectedIncomeDateId { get; set; }

    /// <summary>
    /// Date of the expected income date
    /// </summary>
    [ExcelColumn(ColumnName = "Date")]
    public DateTime Date { get; set; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Employer
    /// </summary>
    [ExcelColumn(ColumnName = "Employer")]
    public Employer Employer { get; } = null!;

    #endregion

    #region Methods

    #endregion
}
