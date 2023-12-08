using Models;

namespace Schema.Entities;

/// <summary>
/// Entity class representing an Employer Income Information.
/// Employer Income Information are children of an Employer Income Rate or an Income.
/// </summary>
public class EmployerIncomeInformation
{
    #region Properties

    /// <inheritdoc cref="IEmployerIncomeInformation.Id"/>
    public long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Income
    /// </summary>
    /// <remark>
    /// If not null, this Income Information belongs to an Income.
    /// An Employer Income Information can only belong to one of an Employer Income Rate or an Income.
    /// </remark>
    public Income? Income { get; } = null!;

    /// <summary>
    /// Navigation to the parent Employer Income Rate
    /// </summary>
    /// <remarks>
    /// If not null, this Income Information belongs to an Employer Income Rate.
    /// An Employer Income Information can only belong to one of an Employer Income Rate or an Income.
    /// </remarks>
    public EmployerIncomeRate? EmployerIncomeRate { get; } = null!;

    /// <inheritdoc cref="IEmployerIncomeInformation.SalaryIncome"/>
    public decimal SalaryIncome { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.AdditionalTaxableIncome"/>
    public decimal AdditionalTaxableIncome { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.RetirementContributionAmount"/>
    public decimal RetirementContributionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PensionContributionAmount"/>
    public decimal PensionContributionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PreTaxDeductionAmount"/>
    public decimal PreTaxDeductionAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.TaxWithholdingAmount"/>
    public decimal TaxWithholdingAmount { get; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PostTaxDeductionAmount"/>
    public decimal PostTaxDeductionAmount { get; }

    #endregion

    #region Methods

    #endregion
}