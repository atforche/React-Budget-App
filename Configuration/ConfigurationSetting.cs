namespace Configuration;

/// <summary>
/// Record that represents a single setting in the App Configuration.
/// </summary>
public record ConfigurationSetting<T>
{
    /// <summary>
    /// Value of the setting if the app is running in a test context
    /// </summary>
    public required T Test { get; init; }

    /// <summary>
    /// Value of the setting if the app is running in a published context
    /// </summary>
    public required T Published { get; init; }
}