using System.CommandLine;

namespace CommandLine;

/// <summary>
/// Helper class to manage command line input. Supports importing data
/// from an Excel sheet into the SQL database or exporting data from the SQL
/// database into an Excel sheet. Also supports uploading a backup file
/// to OneDrive.
/// </summary>
public class CommandLineHelper
{
    public static int Main(string[] args)
    {
        var rootCommand = new RootCommand("Budget App Command Line Interface");
        var excelFileOption = new Option<string>("--file", "Path to the Excel file")
        {
            IsRequired = true
        };
        var dbFileOption = new Option<string>("--dbFile", "Path to the database file")
        {
            IsRequired = true
        };
        var backupFileOption = new Option<string>("--dest", "Destination path for the backup")
        {
            IsRequired = true
        };

        var importCommand = new Command(
            "import",
            "Import the contents of a formatted Excel spreadsheet into the SQL Database"
        )
        {
            excelFileOption,
            dbFileOption
        };
        importCommand.SetHandler(
            (excelFilePath, dbFilePath) =>
            {
                ErrorHandlingWrapper(() => ImportDataFromFile(excelFilePath, dbFilePath));
            },
            excelFileOption,
            dbFileOption
        );
        rootCommand.AddCommand(importCommand);

        var exportCommand = new Command(
            "export",
            "Export the contents of the SQL Database into a formatted Excel spreadsheet"
        )
        {
            excelFileOption,
            dbFileOption
        };
        rootCommand.AddCommand(exportCommand);

        var backupCommand = new Command("backup", "Backup an exported dataset to OneDrive")
        {
            excelFileOption,
            backupFileOption
        };
        rootCommand.AddCommand(backupCommand);

        return rootCommand.Invoke(args);
    }

    /// <summary>
    /// Imports data from an Excel file and populate a SQLite database with the data.
    /// Verifies the file paths are valid.
    /// </summary>
    /// <param name="excelFilePath">Path to the formatted Excel file</param>
    /// <param name="dbFilePath">Path to the SQLite database file</param>
    /// <exception cref="Exception">
    /// Throws a generic exception if the file paths are invalid, the Excel file is incorrectly formatted or
    /// its data is invalid, or the SQL schema is incorrect.
    /// </exception>
    private static void ImportDataFromFile(string excelFilePath, string dbFilePath)
    {
        if (!File.Exists(excelFilePath))
        {
            throw new Exception("Unable to find Excel file at path: \"" + excelFilePath + "\"");
        }
        if (!File.Exists(dbFilePath))
        {
            throw new Exception("Unable to find DB file at path: \"" + dbFilePath + "\"");
        }
        Console.WriteLine("Hello World!");
    }

    /// <summary>
    /// Wrap the given method in a Try/Catch block to handle errors
    /// </summary>
    /// <param name="function">Method to wrap with error handling</param>
    private static void ErrorHandlingWrapper(Action function)
    {
        try
        {
            function();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Stack Trace:");
            Console.WriteLine(e.StackTrace);
        }
    }
}
