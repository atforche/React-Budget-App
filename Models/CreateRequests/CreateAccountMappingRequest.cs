namespace Models.CreateRequests;

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
    long AccountId { get; init; }

    /// <summary>
    /// The parent <see cref="IMonth.Id"/> or <see cref="ICreateMonthRequest.Id"/>
    /// If null, this request must fall under a <see cref="ICreateMonthRequest"/> and the value will be inferred.
    /// </summary>
    long? MonthId { get; init; }

    /// <inheritdoc cref="IAccountMapping.StartingBalance"/>
    decimal StartingBalance { get; init; }

    /// <summary>
    /// The related <see cref="IBudget.Id"/> or <see cref="ICreateBudgetRequest.Id"/>
    /// </summary>
    long? BudgetId { get; init; }

    /// <inheritdoc cref="IAccountMapping.BudgetType"/>
    BudgetType? BudgetType { get; init; }

    /// <inheritdoc cref="IAccountMapping.IsDefault"/>
    bool IsDefault { get; init; }
}

/// <summary>
/// Record class representing a request to create an Account Mapping
/// </summary>
public record CreateAccountMappingRequest : CreateRequestBase, ICreateAccountMappingRequest
{
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