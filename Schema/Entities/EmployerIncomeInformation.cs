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
    public required long Id { get; set; }

    /// <summary>
    /// Navigation to the parent Income
    /// </summary>
    /// <remark>
    /// If not null, this Income Information belongs to an Income.
    /// An Employer Income Information can only belong to one of an Employer Income Rate or an Income.
    /// </remark>
    public required Income? Income { get; set; }

    /// <summary>
    /// Navigation to the parent Employer Income Rate
    /// </summary>
    /// <remarks>
    /// If not null, this Income Information belongs to an Employer Income Rate.
    /// An Employer Income Information can only belong to one of an Employer Income Rate or an Income.
    /// </remarks>
    public required EmployerIncomeRate? EmployerIncomeRate { get; set; }

    /// <inheritdoc cref="IEmployerIncomeInformation.SalaryIncome"/>
    public required decimal SalaryIncome { get; set; }

    /// <inheritdoc cref="IEmployerIncomeInformation.AdditionalTaxableIncome"/>
    public required decimal AdditionalTaxableIncome { get; set; }

    /// <inheritdoc cref="IEmployerIncomeInformation.RetirementContributionAmount"/>
    public required decimal RetirementContributionAmount { get; set; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PensionContributionAmount"/>
    public required decimal PensionContributionAmount { get; set; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PreTaxDeductionAmount"/>
    public required decimal PreTaxDeductionAmount { get; set; }

    /// <inheritdoc cref="IEmployerIncomeInformation.TaxWithholdingAmount"/>
    public required decimal TaxWithholdingAmount { get; set; }

    /// <inheritdoc cref="IEmployerIncomeInformation.PostTaxDeductionAmount"/>
    public required decimal PostTaxDeductionAmount { get; set; }

    #endregion

    #region Methods

    #endregion
}