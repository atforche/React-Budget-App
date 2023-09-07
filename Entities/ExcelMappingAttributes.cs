namespace Entities;

/// <summary>
/// Excel Table annotation used to designate entity classes
/// that map to Excel tables
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
public class ExcelTableAttribute : System.Attribute
{
    #region Properties

    /// <summary>
    /// Excel table name of this entity class
    /// </summary>
    public string TableName { get; set; }

    #endregion

    /// <summary>
    /// Constructs a new instance of this attribute
    /// </summary>
    public ExcelTableAttribute(string tableName)
    {
        TableName = tableName;
    }
}

/// <summary>
/// Excel Column annotation used to designate entity properties
/// that map to columns in an Excel table.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
public class ExcelColumnAttribute : System.Attribute
{
    #region Properties

    /// <summary>
    /// Excel column name of this property
    /// </summary>
    public string ColumnName { get; set; }

    #endregion

    /// <summary>
    /// Constructs a new instance of this attribute
    /// </summary>
    public ExcelColumnAttribute(string columnName)
    {
        ColumnName = columnName;
    }
}
