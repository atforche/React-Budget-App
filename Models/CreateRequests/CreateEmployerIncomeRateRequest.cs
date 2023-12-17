namespace Models.CreateRequests;

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

/// <summary>
/// Record class representing a request to create an Employer Income Rate.
/// </summary>
public record CreateEmployerIncomeRateRequest : CreateRequestBase, ICreateEmployerIncomeRateRequest
{
    /// <inheritdoc/>
    public required long EmployerId { get; init; }

    /// <inheritdoc/>
    public long? MonthId { get; init; }

    /// <inheritdoc/>
    public required ICreateEmployerIncomeInformationRequest CreateEmployerIncomeInformationRequest { get; init; }
}