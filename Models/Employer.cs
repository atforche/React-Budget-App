using Models.Attributes;

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
/// Record class representing an Employer.
/// </summary>
public record Employer : IEmployer
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/> 
    public required string Name { get; init; }
}