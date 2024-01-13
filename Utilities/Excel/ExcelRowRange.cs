namespace Utilities.Excel;

/// <summary>
/// Record class representing a range of row indexes.
/// </summary>
public record ExcelRowRange
{
    #region Properties

    /// <summary>
    /// Starting row index of the range
    /// </summary>
    public int StartingIndex { get; init; }

    /// <summary>
    /// Ending row index of the range (inclusive)
    /// </summary>
    public int EndingIndex { get; init; }

    #endregion

    #region Methods

    /// <summary>
    /// Constructs a new instance of this class
    /// </summary>
    public ExcelRowRange(int startingIndex, int endingIndex)
    {
        StartingIndex = startingIndex;
        EndingIndex = endingIndex;
        Validate();
    }

    /// <summary>
    /// Returns the union of two RowRanges
    /// </summary>
    /// <param name="first">First range to union</param>
    /// <param name="second">Second range to union</param>
    /// <param name="allowNonOverlapping">If true, skips the validation that ensures the provided ranges overlap.
    /// If the ranges don't overlap, any rows caught in-between the two ranges will also be included in the 
    /// union range. If false, an error will be thrown if the two ranges don't overlap</param>
    /// <returns>ExcelRowRange that represents the union between the two provided rows</returns>
    public static ExcelRowRange Union(ExcelRowRange first, ExcelRowRange second, bool allowNonOverlappingRanges = false)
    {
        // Verify that the provided RowRanges overlap
        if (!allowNonOverlappingRanges &&
            (first.StartingIndex > second.EndingIndex || first.EndingIndex < second.StartingIndex))
        {
            throw new Exception("Cannot perform union on RowRanges that don't overlap");
        }
        return new ExcelRowRange(Math.Min(first.StartingIndex, second.StartingIndex),
            Math.Max(first.EndingIndex, second.EndingIndex));
    }

    /// <summary>
    /// Ensures that the current RowRange is valid
    /// </summary>
    private void Validate()
    {
        if (StartingIndex > EndingIndex)
        {
            throw new Exception($"Cannot construct RowRange where StartingIndex ({StartingIndex}) " +
                $"is greater than EndingIndex ({EndingIndex})");
        }
    }

    #endregion
}