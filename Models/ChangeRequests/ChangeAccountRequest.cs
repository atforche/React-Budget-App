namespace Models.ChangeRequests;

/// <summary>
/// Interface representing a request to change an Account.
/// </summary>
public interface IChangeAccountRequest
{
    /// <inheritdoc cref="IAccount.Id"/>
    long Id { get; }

    /// <inheritdoc cref="IAccount.Name"/>
    string Name { get; }
}

/// <summary>
/// Record class representing a request to change an Account.
/// </summary>
public record ChangeAccountRequest : IChangeAccountRequest
{
    /// <inheritdoc/>
    public required long Id { get; init; }

    /// <inheritdoc/>
    public required string Name { get; init; }
}