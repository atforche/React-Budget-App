using System.CommandLine;

namespace CommandLine;

/// <summary>
/// Helper class to manage command line input. Supports importing data
/// from an Excel sheet into the SQL database or exporting data from the SQL
/// database into an Excel sheet. Also supports uploading a backup file
/// to OneDrive.
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
        var excelFileOption = new Option<string>(
            "--importFile",
            "Name of the Excel workbook in the Import directory"
        )
        {
            IsRequired = true
        };
        var exportFileOption = new Option<string>(
            "--exportFile",
            "Filename for the export in the Export directory"
        )
        {
            IsRequired = true
        };
        var backupFileOption = new Option<string>(
            "--output",
            "Filename for the backup file in OneDrive"
        )
        {
            IsRequired = true
        };

        var importCommand = new Command(
            "import",
            "Import the contents of a formatted Excel spreadsheet into the SQL Database"
        )
        {
            excelFileOption
        };
        importCommand.SetHandler(
            (excelFilePath) =>
            {
                ErrorHandlingWrapper(() => DataImporter.Execute(excelFilePath));
            },
            excelFileOption
        );
        rootCommand.AddCommand(importCommand);

        var exportCommand = new Command(
            "export",
            "Export the contents of the SQL Database into a formatted Excel spreadsheet"
        )
        {
            exportFileOption
        };
        rootCommand.AddCommand(exportCommand);

        var backupCommand = new Command("backup", "Backup an exported dataset to OneDrive")
        {
            excelFileOption,
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
