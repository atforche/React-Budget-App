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
    IEmployerIncomeInformation? EmployerIncomeInformation { get; }
}

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
    long? MonthId { get; }

    /// <summary>
    /// The parent <see cref="IEmployer.Id"/> or <see cref="ICreateEmployerRequest.Id"/>
    /// </summary>
    long? EmployerId { get; }

    /// <inheritdoc cref="IIncome.Date"/>
    DateTime Date { get; }

    /// <inheritdoc cref="IIncome.Amount"/>
    decimal Amount { get; }

    /// <inheritdoc cref="IIncome.Description"/>
    string Description { get; }

    /// <summary>
    /// The related <see cref="IAccountMapping.Id"/> or <see cref="ICreateAccountMappingRequest.Id"/>
    /// </summary>
    long? OverrideAccountMappingId { get; }

    /// <inheritdoc cref="IIncome.EmployerIncomeInformation"/>
    ICreateEmployerIncomeInformationRequest? CreateEmployerIncomeInformationRequest { get; }
}

public record CreateIncomeRequest : ICreateIncomeRequest
{
    private static long Sequence = long.MaxValue;

    /// <inheritdoc/>
    public long Id { get; } = Sequence--;

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
    public ICreateEmployerIncomeInformationRequest? CreateEmployerIncomeInformationRequest { get; init; }
}

/// <summary>
/// Interface representing a request to change an Income.
/// </summary>
public interface IChangeIncomeRequest
{
    /// <inheritdoc cref="IIncome.Id"/>
    long Id { get; }

    /// <inheritdoc cref="IIncome.Date"/>
    DateTime Date { get; }

    /// <inheritdoc cref="IIncome.Amount"/>
    decimal Amount { get; }

    /// <inheritdoc cref="IIncome.Description"/>
    string Description { get; }

    /// <inheritdoc cref="IIncome.OverrideAccountMappingId"/>
    long? OverrideAccountMappingId { get; }

    /// <inheritdoc cref="IIncome.EmployerIncomeInformation"/>
    IChangeEmployerIncomeInformationRequest? ChangeEmployerIncomeInformationRequest { get; }
}