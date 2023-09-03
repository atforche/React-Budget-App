namespace RestApi.WindowsService;

/// <summary>
/// Background Service class that allows the REST API to be hosted in a Windows service
/// </summary>
public class WindowsService : BackgroundService
{
    #region Fields

    /// <summary>
    /// Name of this service
    /// </summary>
    private const string ServiceName = "React-Budget-API";

    #endregion

    #region Properties

    /// <summary>
    /// Logger for this service
    /// </summary>
    public ILogger Logger { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Constructs a new instance of this class
    /// </summary>
    public WindowsService(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger<WindowsService>();
    }

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation($"{ServiceName} is starting.");
        stoppingToken.Register(() => Logger.LogInformation($"{ServiceName} is stopping"));
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(3600, stoppingToken);
        }
        Logger.LogInformation($"{ServiceName} has stopped");
    }

    #endregion
}
