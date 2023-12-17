namespace Models.Attributes;

/// <summary>
/// Excel Table annotation used to designate entity classes that map to Excel tables.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
public class ExcelTableAttribute(string tableName, bool isMonthly = true) : Attribute
{
    #region Properties

    /// <summary>
    /// Excel table name of this model class
    /// </summary>
    public string TableName { get; init; } = tableName;

    /// <summary>
    /// True if this entity represents monthly data, false if this entity represents setup data
    /// </summary>
    public bool IsMonthly { get; init; } = isMonthly;

    #endregion
    #region Methods

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return TableName.GetHashCode();
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is ExcelTableAttribute tableAttribute && tableAttribute.TableName == TableName;
    }

    #endregion
}