namespace Entities;

/// <summary>
/// Entity class representing an Account Mapping.
/// Account Mappings are children of Accounts, Budgets, and Months and parents of Transaction Applications.
/// </summary>
public class AccountMapping
{
    #region Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Starting Balance of this Account Mapping
    /// </summary>
    public decimal StartingBalance { get; }

    /// <summary>
    /// Budget Type of this Account Mapping
    /// </summary>
    /// <remarks>
    /// If not null, this Account Mapping will apply to every Budget that falls under the specified Budget Type.
    /// Only one of the BudgetType and Budget may be provided for an Account Mapping.
    /// </remarks>
    public BudgetType? BudgetType { get; }

    /// <summary>
    /// Is Default flag of this Account Mapping
    /// </summary>
    /// <remarks>
    /// If multiple Account Mappings exist for the same Budget or Budget Type, only one may be specified as
    /// the default. The default account mapping will be used by any Transaction Applications against the 
    /// Budget or Budget Type, unless an override is provided on the Application.
    /// </remarks>
    public bool IsDefault { get; }

    #endregion

    #region Navigations

    /// <summary>
    /// Navigation to the parent Account
    /// </summary>
    public Account Account { get; } = null!;

    /// <summary>
    /// Navigation to the parent Month
    /// </summary>
    public Month Month { get; } = null!;

    /// <summary>
    /// Navigation to the parent Budget
    /// </summary>
    /// <remarks>
    /// If not null, this Account Mapping will apply only to the single Budget specified here.
    /// Only one of the BudgetType and Budget may be provided for an Account Mapping.
    /// </remarks>
    public Budget? Budget { get; } = null!;

    /// <summary>
    /// Navigation to the child Transaction Applications
    /// </summary>
    /// <remarks>
    /// This navigation only maps to Transaction Applications that specify this Account Mapping as their override
    /// mapping. This collection should only have Transaction Applications if IsDefault is false.
    /// If IsDefault is true, this collection should always be empty since an override isn't needed to 
    /// use this mapping. 
    /// </remarks>
    private ICollection<TransactionApplication> OverrideTransactionApplications { get; } = null!;

    #endregion

    #region Methods

    #endregion
}