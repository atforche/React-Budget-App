using System.Text.Json.Serialization;

namespace Models;

/// <summary>
/// Read-only interface representing an Account Mapping.
/// </summary>
[ExcelTable("AccountMappings.{Month}")]
public interface IAccountMapping
{
    /// <summary>
    /// Id of the Account Mapping
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Id of the parent Account
    /// </summary>
    [ExcelColumn("Account Name", typeof(string))]
    long AccountId { get; }

    /// <summary>
    /// Id of the parent Month
    /// </summary>
    long MonthId { get; }

    /// <summary>
    /// Starting Balance of the Account Mapping
    /// </summary>
    [ExcelColumn("Starting Balance", typeof(decimal))]
    decimal StartingBalance { get; }

    /// <summary>
    /// Id of the related Budget
    /// </summary>
    /// <remarks>
    /// If not null, this Account Mapping will apply only to the single Budget specified here.
    /// Only one of the BudgetType and Budget may be provided for a regular Account Mapping.
    /// </remarks>
    [ExcelColumn("Budget Name", typeof(string), true)]
    long? BudgetId { get; }

    /// <summary>
    /// Budget Type of the Account Mapping
    /// </summary>
    /// <remarks>
    /// If not null, this Account Mapping will apply to every Budget that falls under the specified Budget Type.
    /// Only one of the BudgetType and Budget may be provided for a regular Account Mapping.
    /// </remarks>
    [ExcelColumn("Budget Type", typeof(string), true)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    BudgetType? BudgetType { get; }

    /// <summary>
    /// Is Default flag of this Account Mapping
    /// </summary>
    /// <remarks>
    /// If multiple Account Mappings exist for the same Budget or Budget Type, only one may be specified as
    /// the default. The default account mapping will be used by any Transaction Applications against the 
    /// Budget or Budget Type, unless an override is provided on the Application.
    /// </remarks>
    [ExcelColumn("Is Default", typeof(bool))]
    bool IsDefault { get; }
}

/// <summary>
/// Interface representing a request to create an Account Mapping.
/// </summary>
public interface ICreateAccountMappingRequest
{
    /// <summary>
    /// Id of the Create Account Mapping Request
    /// </summary>
    long Id { get; }

    /// <summary>
    /// The parent <see cref="IAccount.Id"/> or <see cref="ICreateAccountRequest.Id"/>
    /// </summary>
    long AccountId { get; }

    /// <summary>
    /// The parent <see cref="IMonth.Id"/> or <see cref="ICreateMonthRequest.Id"/>
    /// If null, this request must fall under a <see cref="ICreateMonthRequest"/> and the value will be inferred.
    /// </summary>
    long? MonthId { get; }

    /// <inheritdoc cref="IAccountMapping.StartingBalance"/>
    decimal StartingBalance { get; }

    /// <summary>
    /// The related <see cref="IBudget.Id"/> or <see cref="ICreateBudgetRequest.Id"/>
    /// </summary>
    long? BudgetId { get; }

    /// <inheritdoc cref="IAccountMapping.BudgetType"/>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    BudgetType? BudgetType { get; }

    /// <inheritdoc cref="IAccountMapping.IsDefault"/>
    bool IsDefault { get; }
}

public record CreateAccountMappingRequest : ICreateAccountMappingRequest
{
    private static long Sequence = long.MaxValue;

    /// <inheritdoc/>
    public long Id { get; } = Sequence--;

    /// <inheritdoc/>
    public required long AccountId { get; init; }

    /// <inheritdoc/>
    public long? MonthId { get; init; }

    /// <inheritdoc/>
    public required decimal StartingBalance { get; init; }

    /// <inheritdoc/>
    public long? BudgetId { get; init; }

    /// <inheritdoc/>
    public BudgetType? BudgetType { get; init; }

    /// <inheritdoc/>
    public required bool IsDefault { get; init; }
}

/// <summary>
/// Interface representing a request to change an Account Mapping.
/// </summary>
public interface IChangeAccountMappingRequest
{
    /// <inheritdoc cref="IAccountMapping.Id"/>
    long Id { get; }

    /// <inheritdoc cref="IAccountMapping.StartingBalance"/>
    decimal StartingBalance { get; }

    /// <inheritdoc cref="IAccountMapping.BudgetId"/>
    long? BudgetId { get; }

    /// <inheritdoc cref="IAccountMapping.BudgetType"/>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    BudgetType? BudgetType { get; }

    /// <inheritdoc cref="IAccountMapping.IsDefault"/>
    bool IsDefault { get; }
}