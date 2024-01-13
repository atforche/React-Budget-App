namespace Models.CreateRequests;

/// <summary>
/// Interface representing a request to create an Income.
/// </summary>
public interface ICreateIncomeRequest
{
    /// <summary>
    /// Id of the Create Income Request
    /// </summary>
    long Id { get; }

    /// <summary>
    /// The parent <see cref="IMonth.Id"/> or <see cref="ICreateMonthRequest.Id"/>
    /// If null, this request must fall under a <see cref="ICreateMonthRequest"/> and the value will be inferred.
    /// </summary>
    long? MonthId { get; init; }

    /// <summary>
    /// The parent <see cref="IEmployer.Id"/> or <see cref="ICreateEmployerRequest.Id"/>
    /// </summary>
    long? EmployerId { get; init; }

    /// <inheritdoc cref="IIncome.Date"/>
    DateTime Date { get; init; }

    /// <inheritdoc cref="IIncome.Amount"/>
    decimal Amount { get; init; }

    /// <inheritdoc cref="IIncome.Description"/>
    string Description { get; init; }

    /// <summary>
    /// The related <see cref="IAccountMapping.Id"/> or <see cref="ICreateAccountMappingRequest.Id"/>
    /// </summary>
    long? OverrideAccountMappingId { get; init; }

    /// <inheritdoc cref="IIncome.EmployerIncomeInformation"/>
    ICreateEmployerIncomeInformationRequest? EmployerIncomeInformation { get; init; }
}

/// <summary>
/// Record class representing a request to create an Income.
/// </summary>
public record CreateIncomeRequest : CreateRequestBase, ICreateIncomeRequest
{
    /// <inheritdoc/>
    public long? MonthId { get; init; }

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
    public ICreateEmployerIncomeInformationRequest? EmployerIncomeInformation { get; init; }
}