namespace Models;

/// <summary>
/// Read-only interface representing an Employer.
/// </summary>
[ExcelTable("Employers", false)]
public interface IEmployer
{
    /// <summary>
    /// Id of the Employer
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Name of the Employer
    /// </summary>
    [ExcelColumn("Name", typeof(string))]
    string Name { get; }
}

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
    string Name { get; }
}

public record CreateEmployerRequest : ICreateEmployerRequest
{
    private static long Sequence = 1;

    /// <inheritdoc/>
    public long Id { get; } = Sequence++;

    /// <inheritdoc/>
    public required string Name { get; init; }
}

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