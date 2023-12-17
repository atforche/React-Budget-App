namespace Models.ChangeRequests;

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

/// <summary>
/// Record class representing a request to change an Income.
/// </summary>
public record ChangeIncomeRequest : IChangeIncomeRequest
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required DateTime Date { get; init; }

    /// <inheritdoc/>
    public required decimal Amount { get; init; }

    /// <inheritdoc/>
    public required string Description { get; init; }

    /// <inheritdoc/>
    public long? OverrideAccountMappingId { get; init; }

    /// <inheritdoc/>
    public IChangeEmployerIncomeInformationRequest? ChangeEmployerIncomeInformationRequest { get; init; }
}