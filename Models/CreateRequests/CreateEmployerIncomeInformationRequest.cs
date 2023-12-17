namespace Models.CreateRequests;

/// <summary>
/// Interface representing a request to create an Employer Income Information.
/// </summary>
public interface ICreateEmployerIncomeInformationRequest
{
    /// <summary>
    /// Id of the Create Employer Income Information Request
    /// </summary>
    long Id { get; }

    /// <summary>
    /// The parent <see cref="IIncome.Id"/> or <see cref="ICreateIncomeRequest.Id"/>
    /// If null and this request falls under a <see cref="ICreateIncomeRequest"/>, the value will be inferred.
    /// </summary>
    long? IncomeId { get; }

    /// <summary>
    /// The parent <see cref="IEmployerIncomeRate.Id"/> or <see cref="ICreateEmployerIncomeRateRequest.Id"/>
    /// If null and this request falls under a <see cref="ICreateEmployerIncomeRateRequest"/>, the value will be 
    /// inferred.
    /// </summary>
    long? EmployerIncomeRateId { get; }

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

public record CreateEmployerIncomeInformationRequest : CreateRequestBase, ICreateEmployerIncomeInformationRequest
{
    /// <inheritdoc/>
    public long? IncomeId { get; init; }

    /// <inheritdoc/>  
    public long? EmployerIncomeRateId { get; init; }

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