using Models.Attributes;

namespace Models;

/// <summary>
/// Read-only interface representing an Income.
/// </summary>
[ExcelTable("Incomes.{Month}")]
public interface IIncome
{
    /// <summary>
    /// Id of the Income
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Id of the parent Month
    /// </summary>
    long MonthId { get; }

    /// <summary>
    /// Id of the parent Employer
    /// </summary>
    [ExcelColumn("Employer", typeof(string))]
    long? EmployerId { get; }

    /// <summary>
    /// Date of the Income
    /// </summary>
    [ExcelColumn("Date", typeof(DateTime))]
    DateTime Date { get; }

    /// <summary>
    /// Amount of the Income
    /// </summary>
    [ExcelColumn("Amount", typeof(decimal))]
    decimal Amount { get; }

    /// <summary>
    /// Description of the Income
    /// </summary>
    [ExcelColumn("Description", typeof(string))]
    string Description { get; }

    /// <summary>
    /// Id of the related Override Account Mapping
    /// </summary>
    /// <remarks>
    /// If null, this Income will be credited to the Account mapped to the Spend budget type
    /// If not null, this Income will be credited the Account specified on this Account Mapping.
    /// </remarks>
    [ExcelColumn("Account", typeof(string), true)]
    long? OverrideAccountMappingId { get; }

    /// <summary>
    /// Child Employer Income Information
    /// </summary>
    [ExcelChildModel(true)]
    IEmployerIncomeInformation? EmployerIncomeInformation { get; }
}

/// <summary>
/// Record class representing an Income.
/// </summary>
public record Income : IIncome
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required long MonthId { get; init; }

    /// <inheritdoc/>
    public long? EmployerId { get; init; }

    /// <inheritdoc/>
    public required DateTime Date { get; init; }

    /// <inheritdoc/>
    public required decimal Amount { get; init; }

    /// <inheritdoc/>
    public required string Description { get; init; }

    /// <inheritdoc/>
    public long? OverrideAccountMappingId { get; init; }

    /// <inheritdoc/>
    public IEmployerIncomeInformation? EmployerIncomeInformation { get; init; }
}