namespace Models.ChangeRequests;

/// <summary>
/// Interface representing a request to change an Employer Income Information.
/// </summary>
public interface IChangeEmployerIncomeInformationRequest
{
    /// <inheritdoc cref="IEmployerIncomeInformation.Id"/>
    long Id { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.SalaryIncome"/>
    decimal SalaryIncome { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.AdditionalTaxableIncome"/>
    decimal AdditionalTaxableIncome { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.RetirementContributionAmount"/>
    decimal RetirementContributionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PensionContributionAmount"/>
    decimal PensionContributionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PreTaxDeductionAmount"/>
    decimal PreTaxDeductionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.TaxWithholdingAmount"/>
    decimal TaxWithholdingAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PostTaxDeductionAmount"/>
    decimal PostTaxDeductionAmount { get; }
}

/// <summary>
/// Record class representing a request to change an Employer Income Information.
/// </summary>
public record ChangeEmployerIncomeInformationRequest : IChangeEmployerIncomeInformationRequest
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required decimal SalaryIncome { get; init; }

    /// <inheritdoc/>
    public required decimal AdditionalTaxableIncome { get; init; }

    /// <inheritdoc/>
    public required decimal RetirementContributionAmount { get; init; }

    /// <inheritdoc/>
    public required decimal PensionContributionAmount { get; init; }

    /// <inheritdoc/>
    public required decimal PreTaxDeductionAmount { get; init; }

    /// <inheritdoc/>
    public required decimal TaxWithholdingAmount { get; init; }

    /// <inheritdoc/>
    public required decimal PostTaxDeductionAmount { get; init; }
}