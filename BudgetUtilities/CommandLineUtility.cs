using System.CommandLine;

namespace BudgetUtilities;

/// <summary>
/// Helper class to manage command line input. Supports importing data
/// from an Excel sheet into the SQL database, exporting data from the SQL
/// database into an Excel sheet, and uploading a backup file
/// to OneDrive. Can also configure environment variables as a Post-Build event.
/// </summary>
public class CommandLineUtility
{
    /// <summary>
    /// Entry point for the command line utility.
    /// </summary>
    /// <param name="args">List of command line arguments</param>
    /// <returns>The return status code of the application</returns>
    public static int Main(string[] args)
    {
        RootCommand argumentHandler = ConfigureCommandLineArguments();
        return argumentHandler.Invoke(args);
    }

    /// <summary>
    /// Configures the command line arguments and sets the appropriate handlers.
    /// </summary>
    /// <returns>A RootCommand object configured to handle command line input</returns>
    private static RootCommand ConfigureCommandLineArguments()
    {
        var rootCommand = new RootCommand("React Budget App Command Line Interface");

        // Import Command
        var excelFileOption = new Option<string>("--file", "Name of an Excel workbook in the Import directory")
        {
            IsRequired = true
        };
        var importCommand = new Command("import",
        "Import the contents of a formatted Excel spreadsheet into the SQL Database")
        {
            excelFileOption
        };
        importCommand.SetHandler(
            (excelFilePath) => ErrorHandlingWrapper(() => new DataImportUtility(excelFilePath).Execute()),
            excelFileOption);
        rootCommand.AddCommand(importCommand);

        // Export Command
        var exportFileOption = new Option<string>("--file", "Filename for the export file in the Export directory")
        {
            IsRequired = true
        };
        var exportCommand = new Command("export",
            "Export the contents of the SQL Database into a formatted Excel spreadsheet")
        {
            exportFileOption
        };
        rootCommand.AddCommand(exportCommand);

        // Backup Command
        var backupFileOption = new Option<string>("--output", "Filename for the backup file in OneDrive")
        {
            IsRequired = true
        };
        var backupCommand = new Command("backup", "Backup an exported Excel spreadsheet to OneDrive")
        {
            exportFileOption,
            backupFileOption
        };
        rootCommand.AddCommand(backupCommand);

        return rootCommand;
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
