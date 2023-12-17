namespace Models.Attributes;

/// <summary>
/// Excel Column attribute used to designate entity properties that map to columns in an Excel table.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ExcelColumnAttribute(string columnName, Type columnType, bool isBlankAllowed = false) : Attribute
{
    #region Properties

    /// <summary>
    /// Excel column name of this property
    /// </summary>
    public string ColumnName { get; init; } = columnName;

    /// <summary>
    /// Excel column type of this property
    /// </summary>
    public Type ColumnType { get; init; } = columnType;

    /// <summary>
    /// True if blank is allowed in this Excel column, false otherwise
    /// </summary>
    public bool IsBlankAllowed { get; init; } = isBlankAllowed;

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
