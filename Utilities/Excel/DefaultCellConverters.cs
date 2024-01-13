using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Utilities.Excel;

/// <summary>
/// Static class containing standard lambdas used to convert a value in an Excel column into a property value.
/// </summary>
public static class DefaultColumnConverters
{
    /// <summary>
    /// Default converter lambda for properties with a string value
    /// </summary>
    public static readonly Func<XSSFCell, string> DefaultStringPropertyConverter = cell => cell.StringCellValue;

    /// <summary>
    /// Default converter lambda for properties with a decimal value
    /// </summary>
    public static readonly Func<XSSFCell, object> DefaultDecimalPropertyConverter =
        cell => (decimal)cell.NumericCellValue;

    /// <summary>
    /// Default converter lambda for properties with a boolean value
    /// </summary>
    public static readonly Func<XSSFCell, object> DefaultBooleanPropertyConverter = cell => cell.BooleanCellValue;

    /// <summary>
    /// Default converter lambda for properties with a DateTime value
    /// </summary>
    public static readonly Func<XSSFCell, object> DefaultDateTimePropertyConverter =
        cell => DateTime.FromOADate(cell.NumericCellValue);

    /// <summary>
    /// Gets a default converter lambda for properties with the given Enum type
    /// </summary>
    public static Func<XSSFCell, object> DefaultEnumPropertyConverter<T>() where T : struct =>
        cell => Enum.Parse<T>(cell.StringCellValue.Replace(" ", ""));

    /// <summary>
    /// Given a converter method, returns a converter method that handles nullable values
    /// </summary>
    /// <param name="baseConverter">Base converter method</param>
    /// <returns>Converter method that returns null for blank cells</returns>
    public static Func<XSSFCell, object?> AsNullableConverter(Func<XSSFCell, object> baseConverter) =>
        cell => cell.CellType == CellType.Blank ? null : baseConverter(cell);
}