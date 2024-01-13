namespace Models.Attributes;

/// <summary>
/// Excel Child Model attribute used to designate entity properties that map to a child model in an Excel table.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ExcelChildModelAttribute(bool isOneToOne = false) : Attribute
{
    /// <summary>
    /// True if this child model is one-to-one with the parent model. False otherwise.
    /// </summary>
    public bool IsOneToOne { get; } = isOneToOne;
}
