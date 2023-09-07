namespace Entities;

/// <summary>
/// Entity class representing an Actual Income.
/// Actual Incomes are children of a Month.
/// </summary>
[ExcelTable(TableName = "Actual.Income")]
public class ActualIncome : Income
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long ActualIncomeId { get; set; }

    /// <summary>
    /// Date of the income
    /// </summary>
    [ExcelColumn(ColumnName = "Date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Amount representing the Retirement Contributions of the actual income
    /// </summary>
    [ExcelColumn(ColumnName = "Retirement Contributions")]
    public decimal RetirementContributions { get; set; }

    /// <summary>
    /// Amount representing the Taxes of the actual income
    /// </summary>
    [ExcelColumn(ColumnName = "Taxes")]
    public decimal Taxes { get; set; }

    #endregion

    #region Methods

    #endregion
}
