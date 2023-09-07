using System.Collections;

namespace Entities;

/// <summary>
/// Entity class representing an Expected Income Rate.
/// Expected Income Rates are children of a Month.
/// </summary>
[ExcelTable(TableName = "Expected.Income.Rates")]
public class ExpectedIncomeRate : Income
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long ExpectedIncomeRateId { get; set; }

    /// <summary>
    /// Percentage representing the Retirement Contributions of the expected income rate
    /// </summary>
    [ExcelColumn(ColumnName = "Retirement Contribution Rate")]
    public decimal RetirementContributionRate { get; set; }

    /// <summary>
    /// Percentage representing the Tax Rate of this expected income rate
    /// </summary>
    [ExcelColumn(ColumnName = "Tax Rate")]
    public decimal TaxRate { get; set; }

    #endregion

    #region Methods

    #endregion
}
