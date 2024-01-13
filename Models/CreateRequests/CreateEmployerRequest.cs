namespace Models.CreateRequests;

/// <summary>
/// Interface representing a request to create an Employer.
/// </summary>
public interface ICreateEmployerRequest
{
    /// <summary>
    /// Id of the Create Employer Request
    /// </summary>
    long Id { get; }

    /// <inheritdoc cref="IEmployer.Name"/>
    string Name { get; init; }
}

/// <summary>
/// Record class representing a request to create an Employer.
/// </summary>
public record CreateEmployerRequest : CreateRequestBase, ICreateEmployerRequest
{
    /// <inheritdoc/>
    public required string Name { get; init; }
}