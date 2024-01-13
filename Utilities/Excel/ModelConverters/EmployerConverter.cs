using Models;
using Models.CreateRequests;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Converter class to convert Employer models to and from Excel format.
/// </summary>
/// <param name="worksheet">Excel worksheet containing model data</param>
public class EmployerConverter(XSSFSheet worksheet) : ExcelModelConverter<IEmployer, ICreateEmployerRequest>(worksheet)
{
    #region Properties

    /// <inheritdoc/>
    protected override Dictionary<string, Func<XSSFCell, object?>> ColumnConverters =>
        new()
        {
            [nameof(IEmployer.Name)] = DefaultColumnConverters.DefaultStringPropertyConverter,
        };

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override ICreateEmployerRequest GetDefaultCreateRequest() =>
        new CreateEmployerRequest()
        {
            Name = ""
        };

    #endregion
}