using Models.Attributes;

namespace Models;

/// <summary>
/// Read-only interface representing an Employer Income Rate.
/// </summary>
[ExcelTable("EmployerIncomeRates.{Month}")]
public interface IEmployerIncomeRate
{
    /// <summary>
    /// Id of the Employer Income Rate
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Id of the parent Employer
    /// </summary>
    [ExcelColumn("Employer", typeof(string))]
    long EmployerId { get; }

    /// <summary>
    /// Id of the parent Month
    /// </summary>
    long MonthId { get; }

    /// <summary>
    /// Child Employer Income Information
    /// </summary>
    IEmployerIncomeInformation EmployerIncomeInformation { get; }
}

/// <summary>
/// Record class representing an EmployerIncomeRate.
/// </summary>
public record EmployerIncomeRate : IEmployerIncomeRate
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required long EmployerId { get; init; }

    /// <inheritdoc/>
    public required long MonthId { get; init; }

    /// <inheritdoc/>
    public required IEmployerIncomeInformation EmployerIncomeInformation { get; init; }
}