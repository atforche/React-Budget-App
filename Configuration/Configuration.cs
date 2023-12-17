namespace Configuration;

/// <summary>
/// Record that contains all the Configuration Settings for the application.
/// </summary>
public record Configuration
{
    /// <summary>
    /// Configuration setting that specifies the current app directory
    /// </summary>
    public required ConfigurationSetting<string> AppDirectory { get; init; }

    /// <summary>
    /// Configuration setting that specifies the name of the service running the REST API
    /// </summary>
    public required ConfigurationSetting<string> RestApiServiceName { get; init; }

    /// <summary>
    /// Configuration setting that specifies the URL for the REST API
    /// </summary>
    public required ConfigurationSetting<string> RestApiUrl { get; init; }

    /// <summary>
    /// Configuration setting that specifies the port for the REST API
    /// </summary>
    public required ConfigurationSetting<int> RestApiPort { get; init; }
}