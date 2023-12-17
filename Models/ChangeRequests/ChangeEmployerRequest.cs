namespace Models.ChangeRequests;

/// <summary>
/// Interface representing a request to change an Employer.
/// </summary>
public interface IChangeEmployerRequest
{
    /// <inheritdoc cref="IEmployer.Id"/>
    long Id { get; }

    /// <inheritdoc cref="IEmployer.Name"/>
    string Name { get; }
}

/// <summary>
/// Record class representing a request to change an Employer.
/// </summary>
public record ChangeEmployerRequest : IChangeEmployerRequest
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required string Name { get; init; }
}