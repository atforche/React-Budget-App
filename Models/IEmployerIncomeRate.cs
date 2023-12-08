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
/// Interface representing a request to create an Employer Income Rate.
/// </summary>
public interface ICreateEmployerIncomeRateRequest
{
    /// <summary>
    /// Id of the Create Employer Income Rate Request
    /// </summary>
    long Id { get; }

    /// <summary>
    /// The parent <see cref="IEmployer.Id"/> or <see cref="ICreateEmployerRequest.Id"/>
    /// </summary>
    long EmployerId { get; }

    /// <summary>
    /// The parent <see cref="IMonth.Id"/> or <see cref="ICreateMonthRequest.Id"/>
    /// If null, this request must fall under a <see cref="ICreateMonthRequest"/> and the value will be inferred.
    /// </summary>
    long? MonthId { get; }

    /// <inheritdoc cref="IEmployerIncomeRate.EmployerIncomeInformation"/>
    ICreateEmployerIncomeInformationRequest CreateEmployerIncomeInformationRequest { get; }
}

public record CreateEmployerIncomeRateRequest : ICreateEmployerIncomeRateRequest
{
    private static long Sequence = long.MaxValue;

    /// <inheritdoc/>
    public long Id { get; } = Sequence--;

    /// <inheritdoc/>
    public required long EmployerId { get; init; }

    /// <inheritdoc/>
    public long? MonthId { get; init; }

    /// <inheritdoc/>
    public required ICreateEmployerIncomeInformationRequest CreateEmployerIncomeInformationRequest { get; init; }
}

/// <summary>
/// Interface representing a request to change an Employer Income Rate.
/// </summary>
public interface IChangeEmployerIncomeRateRequest
{
    /// <inheritdoc cref="IEmployerIncomeRate.Id"/>
    long Id { get; }

    /// <inheritdoc cref="IEmployerIncomeRate.EmployerIncomeInformation"/>
    IChangeEmployerIncomeInformationRequest ChangeEmployerIncomeInformationRequest { get; }
}