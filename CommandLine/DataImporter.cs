using System.Diagnostics.CodeAnalysis;
using NPOI.XSSF.UserModel;

namespace CommandLine;

/// <summary>
/// Class that manages importing data from an Excel spreadsheet and uploads it
/// to the database. Validates the Excel spreadsheet to ensure that it's formatted
/// correctly.
/// </summary>
public class DataImporter
{
    #region Fields

    /// <summary>
    /// Path to the directory where any import files will be located.
    /// </summary>
    private const string ImportDirectoryPath = "..\\..\\Import\\";

    #endregion

    #region Properties

    #endregion

    #region Methods

    /// <summary>
    /// Execute the data import action.
    /// Imports data from the provided Excel spreadsheet and uploads it to the SQL database through
    /// the REST API. Validates that each sheet in the Excel spreadsheet is formatted correctly.
    /// </summary>
    /// <param name="excelFileName">Name of the Excel workbook in the Import directory</param>
    public static void Execute(string excelFileName)
    {
        Validate(excelFileName, out XSSFWorkbook? excelWorkbook);
    }

    /// <summary>
    /// Validates the provided Excel file. If the file is determined to have a valid format,
    /// it will also return the imported workbook as an out parameter.
    /// </summary>
    /// <param name="excelFileName">Name of the Excel workbook in the Import directory</param>
    /// <param name="excelWorkbook">If the provided workbook is valid, a XSSFWorkbook object that
    /// can be used to interact with the workbook. Null otherwise.</param>
    public static void Validate(
        string excelFileName,
        [NotNullWhen(true)] out XSSFWorkbook? excelWorkbook
    )
    {
        excelWorkbook = null;
        string fullPath = ImportDirectoryPath + excelFileName;
        if (!File.Exists(fullPath))
        {
            throw new Exception(
                "Unable to find Excel workbook in Import directory: \"" + excelFileName + "\""
            );
        }
        Console.WriteLine("Hello World!");
    }

    #endregion
}
