namespace Models.Attributes;

/// <summary>
/// Excel Table annotation used to designate entity classes that map to Excel tables.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
public class ExcelTableAttribute(string tableName, bool isSetup = false, Type? parentType = null) : Attribute
{
    #region Properties

    /// <summary>
    /// Excel table name of this model class
    /// </summary>
    public string TableName { get; init; } = tableName;

    /// <summary>
    /// True if this entity represents setup data, false if this entity represents monthly data
    /// </summary>
    public bool IsSetup { get; init; } = isSetup;

    /// <summary>
    /// The parent entity of this model class if this model appears on the same Excel table as 
    /// its parent, null otherwise
    /// </summary>
    public Type? ParentType { get; init; } = parentType;

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