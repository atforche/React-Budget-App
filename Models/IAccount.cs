using System.Text.Json.Serialization;

namespace Models;

/// <summary>
/// Read-only interface representing an Account.
/// </summary>
[ExcelTable("Accounts", false)]
public interface IAccount
{
    /// <summary>
    /// Id of the Account
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Name of the Account
    /// </summary>
    [ExcelColumn("Name", typeof(string))]
    string Name { get; }

    /// <summary>
    /// Type of the Account
    /// </summary>
    [ExcelColumn("Type", typeof(string))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    AccountType Type { get; }
}

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

public record CreateAccountRequest : ICreateAccountRequest
{
    private static long Sequence = 1;

    /// <inheritdoc/>
    public long Id { get; } = Sequence++;

    /// <inheritdoc/>
    public required string Name { get; init; }

    /// <inheritdoc/>
    public required AccountType Type { get; init; }
}

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
/// Enum representing the different types of Accounts
/// </summary>
public enum AccountType
{
    /// <summary>
    /// Regular accounts represent any account that is mapped to a particular Budget or Budget Type. The expected balance
    /// of these accounts can be determined using the starting balance of the Account Mapping and any Transactions against
    /// the mapped Budget or Budget Type.
    /// </summary>
    Regular,

    /// <summary>
    /// Credit Card accounts represent any credit card account. These accounts serve as a spending buffer, so any
    /// balance that they carry offsets debits from the Spending account. Balances for these accounts must be
    /// manually entered every week.
    /// </summary>
    CreditCard,

    /// <summary>
    /// Retirement accounts represent any 401(k), 403(b), 457, or pension accounts. These accounts grow with pre-tax
    /// contributions, employer contributions, and changes in the stock market. Balances for these accounts must be
    /// manually entered every week.
    /// </summary>
    Retirement,

    /// <summary>
    /// Loan accounts represent any loan or mortgage accounts. These accounts decrease with regular payments and 
    /// increase through accrued interest. Balances for these accounts must be manually entered every week.
    /// </summary>
    Loan
}