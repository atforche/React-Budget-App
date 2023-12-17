using System.Text.Json.Serialization;

namespace Models.CreateRequests;

/// <summary>
/// Interface representing a request to create an Account.
/// </summary>
public interface ICreateAccountRequest
{
    /// <summary>
    /// Id of the Create Account Request
    /// </summary>
    long Id { get; }

    /// <inheritdoc cref="IAccount.Name"/>
    string Name { get; }

    /// <inheritdoc cref="IAccount.Type"/>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    AccountType Type { get; }
}

/// <summary>
/// Record class representing a request to create an Account.
/// </summary>
public record CreateAccountRequest : CreateRequestBase, ICreateAccountRequest
{
    /// <inheritdoc/>
    public required string Name { get; init; }

    /// <inheritdoc/>
    public required AccountType Type { get; init; }
}