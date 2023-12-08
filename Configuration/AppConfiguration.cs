using System.Reflection;
using System.Text.Json;

namespace Configuration;

/// <summary>
/// Static class used to access different configuration settings about 
/// the currently running application context
/// </summary>
public static class AppConfiguration
{
    #region Fields

    /// <summary>
    /// True if the current app context is the published version. False otherwise.
    /// </summary>
    private readonly static bool IsPublished;

    /// <summary>
    /// Filename pointing to the Config file. It should get copied to the DLL folder during a build step
    /// </summary>
    private const string ConfigFileName = "config.json";

    #endregion

    #region Properties

    /// <summary>
    /// Main directory for the current app context. Child directories will be found beneath this main directory
    /// </summary>
    public static string AppDirectory { get; }

    /// <summary>
    /// Import directory for the current app context
    /// </summary>
    public static string ImportDirectory { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Static constructor of this class. Instantiates all static members when class is first referenced
    /// </summary>
    static AppConfiguration()
    {
        string configFileString = File.ReadAllText(ConfigFileName);
        Configuration currentConfiguration = JsonSerializer.Deserialize<Configuration>(configFileString) ??
            throw new Exception($"Unable to build configuration from the following JSON string: \"{configFileString}\"");

        ValidateConfiguration(currentConfiguration);

        // Check if this is a published context by seeing if the current executing assembly's path contains the 
        // published app directory path.
        IsPublished = AppDomain.CurrentDomain.BaseDirectory.Contains(
            currentConfiguration?.AppDirectory?.Published ?? throw new InvalidOperationException());
        AppDirectory = IsPublished
            ? currentConfiguration?.AppDirectory?.Published ?? throw new InvalidOperationException()
            : currentConfiguration?.AppDirectory?.Test ?? throw new InvalidOperationException();
        ImportDirectory = AppDirectory + "\\Import";
    }

    /// <summary>
    /// Validates that all the configuration properties are specified. Need to do this manually since the JsonRequired
    /// attribute only exists in .NET 7.0
    /// </summary>
    /// <param name="configuration">Configuration model loaded from the config file</param>
    private static void ValidateConfiguration(Configuration configuration)
    {
        foreach (PropertyInfo propertyInfo in typeof(Configuration).GetProperties())
        {
            ConfigurationSetting? setting = propertyInfo.GetValue(configuration) as ConfigurationSetting;
            if (setting == null)
            {
                throw new Exception($"Configuration setting \"{propertyInfo.Name}\" is not provided.");
            }
            if (setting.Test == null)
            {
                throw new Exception($"Test setting for \"{propertyInfo.Name}\" is not provided.");
            }
            if (setting.Published == null)
            {
                throw new Exception($"Published setting for \"{propertyInfo.Name}\" is not provided.");
            }
        }
    }

    #endregion
}