namespace Models;

/// <summary>
/// Excel Table annotation used to designate entity classes that map to Excel tables
/// </summary>
[AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
public class ExcelTableAttribute : Attribute
{
    #region Properties

    /// <summary>
    /// Excel table name of this model class
    /// </summary>
    public string TableName { get; init; }

    /// <summary>
    /// Whether this entity class represents monthly data
    /// </summary>
    public bool IsMonthly { get; init; }

    /// <summary>
    /// Parent Type of the model class when it appears on this table
    /// </summary>
    /// <remarks>
    /// When the same model appears on multiple Excel tables, that model is generally
    /// parented under different parent models in each table.
    /// </remarks>
    public Type? ParentType { get; init; }

    #endregion

    #region Methods

    /// <summary>
    /// Creates a new instance of this class
    /// </summary>
    public ExcelTableAttribute(string tableName, bool isMonthly = true, Type? parentType = null)
    {
        TableName = tableName;
        IsMonthly = isMonthly;
        ParentType = parentType;
    }

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
[AttributeUsage(AttributeTargets.Property)]
public class ExcelColumnAttribute : Attribute
{
    #region Properties

    /// <summary>
    /// Excel column name of this property
    /// </summary>
    public string ColumnName { get; init; }

    /// <summary>
    /// Excel column type of this property
    /// </summary>
    public Type ColumnType { get; init; }

    /// <summary>
    /// Is Blank Allowed in this Excel column
    /// </summary>
    public bool IsBlankAllowed { get; init; }

    #endregion

    #region Methods

    /// <summary>
    /// Constructs a new instance of this class
    /// </summary>
    public ExcelColumnAttribute(string columnName, Type columnType, bool isBlankAllowed = false)
    {
        ColumnName = columnName;
        ColumnType = columnType;
        IsBlankAllowed = isBlankAllowed;
    }

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
