using System;

namespace PowerCollections
{
    /// <summary>
    /// Options for the Between extension methods
    /// </summary>
    [Flags]
    public enum BetweenOptions : int
    {
        None = 0x0,

        /// <summary>
        /// Yield an empty list when the haystack starts with a splitter
        /// </summary>
        YieldStartEvenIfEmpty = 0x1,

        /// <summary>
        /// Yield an empty list when the haystack stops with a splitter
        /// </summary>
        YieldStopEvenIfEmpty = 0x2,

        /// <summary>
        /// Yield the splitter items with the previous list
        /// </summary>
        YieldSplitterWithPreviousItem = 0x4,

        /// <summary>
        /// Yield the splitter items with the next list
        /// </summary>
        YieldSplitterWithNextItem = 0x8,
    }
}