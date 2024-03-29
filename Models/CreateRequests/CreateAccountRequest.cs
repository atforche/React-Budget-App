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
    string Name { get; init; }

    /// <inheritdoc cref="IAccount.Type"/>
    AccountType Type { get; init; }
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