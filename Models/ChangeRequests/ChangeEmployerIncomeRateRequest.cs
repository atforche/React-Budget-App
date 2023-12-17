namespace Models.ChangeRequests;

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

/// <summary>
/// Record class representing a request to change an Employer Income Rate.
/// </summary>
public record ChangeEmployerIncomeRateRequest : IChangeEmployerIncomeRateRequest
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required IChangeEmployerIncomeInformationRequest ChangeEmployerIncomeInformationRequest { get; init; }
}