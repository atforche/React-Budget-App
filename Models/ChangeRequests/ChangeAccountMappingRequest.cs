using System.Text.Json.Serialization;

namespace Models.ChangeRequests;

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

/// <summary>
/// Record class representing a request to change an Account Mapping.
/// </summary>
public record ChangeAccountMappingRequest : IChangeAccountMappingRequest
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required decimal StartingBalance { get; init; }

    /// <inheritdoc/>
    public long? BudgetId { get; init; }

    /// <inheritdoc/>
    public BudgetType? BudgetType { get; init; }

    /// <inheritdoc/>
    public required bool IsDefault { get; init; }
}