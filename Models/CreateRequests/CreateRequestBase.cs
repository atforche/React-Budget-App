namespace Models.CreateRequests;

/// <summary>
/// Base class containing shared functionality for Create Requests.
/// </summary>
public abstract record CreateRequestBase
{
    #region Fields

    /// <summary>
    /// Static sequence used to create an auto-incrementing Id of Create Requests
    /// </summary>
    private static long Sequence = long.MaxValue;

    #endregion

    #region Properties

    /// <summary>
    /// Id for this Create Request
    /// </summary>
    public long Id { get; } = Sequence--;

    #endregion;
}