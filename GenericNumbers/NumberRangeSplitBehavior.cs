using System;

namespace GenericNumbers
{
    [Flags]
    public enum NumberRangeSplitBehavior
    {
        None = 0x0,
        IncludeSplitterInFirstRange = 0x1,
        IncludeSplitterInSecondRange = 0x2,
        IncludeSplitterInBothRanges = IncludeSplitterInFirstRange | IncludeSplitterInSecondRange
    }
}
