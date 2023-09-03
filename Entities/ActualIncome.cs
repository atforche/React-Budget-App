/// <summary>
/// Entity class representing an Actual Income.
/// Actual Incomes are children of a Month.
/// </summary>
public class ActualIncome : Income
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long ActualIncomeId { get; set; }

    /// <summary>
    /// Amount representing the Retirement Contributions of the actual income
    /// </summary>
    public decimal RetirementContributions { get; set; }

    /// <summary>
    /// Amount representing the Taxes of the actual income
    /// </summary>
    public decimal Taxes { get; set; }

    #endregion

    #region Methods

    #endregion
}
