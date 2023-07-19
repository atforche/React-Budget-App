using System;

namespace CommandLine;

/// <summary>
/// Helper class to manage command line input. Enables data to be imported
/// from an Excel sheet into the SQL database or exported from the SQL database
/// into an Excel sheet.
/// </summary>
public class CommandLineHelper
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        if (args.Length > 0)
        {
            Console.WriteLine(args[0]);
        }
    }
}
