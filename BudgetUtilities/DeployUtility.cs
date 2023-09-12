namespace BudgetUtilities;

/// <summary>
/// Class that manages the steps needed to deploy the application to
/// a folder during the Post-Build step. Includes creating any folders
/// and configuring environment variables.
/// </summary>
public class DeployUtility
{
    #region Properties

    /// <summary>
    /// Directory the application is being deployed into
    /// </summary>
    private string DeployDirectory { get; init; }

    /// <summary>
    /// Whether this deployment is publishing the application or not
    /// </summary>
    private bool IsPublish { get; init; }

    /// <summary>
    /// Path of the import directory
    /// </summary>
    private string ImportDirectoryPath { get; init; }

    /// <summary>
    /// Path of the export directory
    /// </summary>
    private string ExportDirectoryPath { get; init; }

    /// <summary>
    /// Name of the environment variable to set during deployment
    /// </summary>
    private string EnvironmentVariableName { get; init; }

    #endregion

    #region Methods

    /// <summary>
    /// Constructs a new instance of this class
    /// </summary>
    /// <param name="deployDirectory">Directory path the application is being deployed to</param>
    /// <param name="isPublish">Whether this deployment is publishing the application or not</param>
    public DeployUtility(string deployDirectory, bool isPublish)
    {
        DeployDirectory = deployDirectory;
        IsPublish = isPublish;
        ImportDirectoryPath = $"{DeployDirectory}\\Import";
        ExportDirectoryPath = $"{DeployDirectory}\\Export";
        EnvironmentVariableName = IsPublish ? "BudgetAppPublishDirectory" : "BudgetAppLocalDirectory";
    }

    /// <summary>
    /// Deploys the application to the provided folder
    /// </summary>
    public void DeployApplication()
    {
        // Ensure that the Import directory has been created
        Directory.CreateDirectory(ImportDirectoryPath);

        // Ensure that the Export directory has been created
        Directory.CreateDirectory(ExportDirectoryPath);

        // Create an environment variable pointing to the app directory
        Environment.SetEnvironmentVariable(EnvironmentVariableName, DeployDirectory, EnvironmentVariableTarget.User);
    }

    #endregion
}