using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Budget models to and from Excel format.
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
public class BudgetConverter(XSSFSheet worksheet) : ExcelModelConverter<IBudget, ICreateBudgetRequest>(worksheet)
{
    #region Properties

    /// <inheritdoc/>
    protected override Dictionary<string, Func<XSSFCell, object?>> ColumnConverters =>
        new()
        {
            [nameof(IBudget.Name)] = DefaultColumnConverters.DefaultStringPropertyConverter,
            [nameof(IBudget.Type)] = DefaultColumnConverters.DefaultEnumPropertyConverter<BudgetType>(),
            [nameof(IBudget.Amount)] = DefaultColumnConverters.DefaultDecimalPropertyConverter,
            [nameof(IBudget.RolloverAmount)] = DefaultColumnConverters.AsNullableConverter(
                DefaultColumnConverters.DefaultDecimalPropertyConverter),
            [nameof(IBudget.IsRolloverAmountOverridden)] = DefaultColumnConverters.AsNullableConverter(
                DefaultColumnConverters.DefaultBooleanPropertyConverter)
        };

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override ICreateBudgetRequest GetDefaultCreateRequest() =>
        new CreateBudgetRequest()
        {
            Name = "",
            Type = BudgetType.Fixed,
            Amount = 0.00m,
            RolloverAmount = null,
            IsRolloverAmountOverridden = null
        };

    #endregion
}