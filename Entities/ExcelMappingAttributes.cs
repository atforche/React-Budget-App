namespace Entities;

/// <summary>
/// Excel Table annotation used to designate entity classes that map to Excel tables
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ExcelTableAttribute : Attribute
{
    #region Properties

    /// <summary>
    /// Excel table name of this entity class
    /// </summary>
    public string TableName { get; set; } = null!;

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

/// <summary>
/// Excel Column annotation used to designate entity properties that map to columns in an Excel table.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class ExcelColumnAttribute : Attribute
{
    #region Properties

    /// <summary>
    /// Excel column name of this property
    /// </summary>
    public string ColumnName { get; set; } = null!;

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return ColumnName.GetHashCode();
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is ExcelColumnAttribute columnAttribute && columnAttribute.ColumnName == ColumnName;
    }

    #endregion
}
