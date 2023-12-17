using System.Text.Json;

namespace Configuration;

/// <summary>
/// Static class used to access different configuration settings about the currently running application context.
/// </summary>
public static class AppConfiguration
{
    #region Fields

    /// <summary>
    /// File name pointing to the config file
    /// </summary>
    private const string ConfigFileName = "config.json";

    /// <summary>
    /// True if the current app context is the published version, false otherwise
    /// </summary>
    private static readonly bool isPublished;

    /// <summary>
    /// Configuration object representing the currently loaded configuration
    /// </summary>
    private static readonly Configuration currentConfiguration;

    #endregion

    #region Properties

    /// <summary>
    /// Main directory for the current app context. Child directories will be found beneath this main directory
    /// </summary>
    public static string AppDirectory => isPublished
        ? currentConfiguration.AppDirectory.Published
        : currentConfiguration.AppDirectory.Test;

    /// <summary>
    /// Import directory for the current app context
    /// </summary>
    public static string ImportDirectory => AppDirectory + "\\Import";

    #endregion

    #region Methods

    /// <summary>
    /// Static constructor of this class. Instantiates all static members when class is first referenced
    /// </summary>
    static AppConfiguration()
    {
        // Read and deserialize the config file
        string configFileString = File.ReadAllText(ConfigFileName);
        currentConfiguration = JsonSerializer.Deserialize<Configuration>(configFileString) ??
            throw new Exception($"Unable to build configuration from the following JSON: \"{configFileString}\"");

        // Check if this is a published context by seeing if the current executing assembly's path contains the 
        // published app directory path.
        isPublished = AppDomain.CurrentDomain.BaseDirectory.Contains(
            currentConfiguration?.AppDirectory?.Published ?? throw new InvalidOperationException());
    }

    #endregion
}