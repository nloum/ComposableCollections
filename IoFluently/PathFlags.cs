using System;

namespace IoFluently
{
    [Flags]
    public enum PathFlags : ulong
    {
        None = 0,

        /// <summary>
        ///     If set, makes the path case-sensitive.
        ///     If not set, implies case-insensitivity.
        /// </summary>
        CaseSensitive = 0x1,

        /// <summary>
        ///     This is used to tell APIs that create AbsolutePath objects to calculate
        ///     the flags based on the given path and environment.
        ///     AbsolutePath.Flags properties will never have this flag set because it
        ///     is only used by the APIs that create AbsolutePaths to calculate the actual
        ///     value of AbsolutePath.Flags.
        /// </summary>
        UseDefaultsForGivenPath = 0x2,

        /// <summary>
        ///     This is used to tell the APIs that create AbsolutePath objects to get
        ///     the flags from the PathUtility.DefaultFlags property.
        ///     AbsolutePath.Flags properties will never have this flag set because it
        ///     is only used by the APIs that create AbsolutePaths to calculate the actual
        ///     value of AbsolutePath.Flags.
        /// </summary>
        UseDefaultsFromUtility = 0x4
    }
}