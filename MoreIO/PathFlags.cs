using System;

namespace MoreIO
{
    [Flags]
	public enum PathFlags : ulong
	{
		None = 0,

        /// <summary>
        /// If set, makes the path case-sensitive.
        /// If not set, implies case-insensitivity.
        /// </summary>
        CaseSensitive = 0x1,
        
        /// <summary>
        /// This is used to tell APIs that create PathSpec objects to calculate
        /// the flags based on the given path and environment.
        /// PathSpec.Flags properties will never have this flag set because it
        /// is only used by the APIs that create PathSpecs to calculate the actual
        /// value of PathSpec.Flags.
        /// </summary>
        UseDefaultsForGivenPath = 0x2,

        /// <summary>
        /// This is used to tell the APIs that create PathSpec objects to get
        /// the flags from the PathUtility.DefaultFlags property.
        /// PathSpec.Flags properties will never have this flag set because it
        /// is only used by the APIs that create PathSpecs to calculate the actual
        /// value of PathSpec.Flags.
        /// </summary>
        UseDefaultsFromUtility = 0x4,
	}
}
