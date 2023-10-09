namespace Configuration;

/// <summary>
/// Record that represents the set of Configuration Settings required to run the application.
/// </summary>
public record Configuration
{
    /// <summary>
    /// Configuration setting that specifies the current app directory
    /// </summary>
    public ConfigurationSetting? AppDirectory { get; set; }
}

/// <summary>
/// Record that represents a single setting in the App Configuration.
/// </summary>
public record ConfigurationSetting
{
    /// <summary>
    /// Value of the setting if the app is running in a local context
    /// </summary>
    public string? Local { get; set; }

    /// <summary>
    /// Value of the setting if the app is running in a published context
    /// </summary>
    public string? Published { get; set; }
}