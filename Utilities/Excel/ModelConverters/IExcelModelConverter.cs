using NPOI.XSSF.UserModel;

namespace Utilities.Excel.ModelConverters;

/// <summary>
/// Interface representing a class that converts between data models and Excel tables.
/// </summary>
public interface IExcelModelConverter
{
    /// <summary>
    /// Converts data in the provided Excel worksheet into a list of data models
    /// </summary>
    /// <param name="bounds">If provided, only data found within the provided range of rows will be converted</param>
    /// <param name="exceptions">Exceptions encountered during the conversion process</param>
    /// <returns>An IEnumerable of objects containing the converted models</returns>
    IEnumerable<object> ConvertExcelToModels(ExcelRowRange? bounds, out IEnumerable<Exception> exceptions);

    /// <summary>
    /// Converts the provided models into Excel format in the provided table
    /// </summary>
    /// <param name="models">Models to convert into Excel format</param>
    /// <param name="table">Excel table to store the converted models</param>
    /// <param name="exceptions">Exceptions encountered during the conversion process</param>
    void ConvertModelsToExcel(IEnumerable<object> models, XSSFTable table, out IEnumerable<Exception> exceptions);
}