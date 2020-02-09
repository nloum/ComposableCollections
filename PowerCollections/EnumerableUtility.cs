using System;

namespace PowerCollections
{
    /// <summary>
    ///     This is where you'll find static properties and static non-extension-methods
    ///     that deal with IEnumerables and types that implement IEnumerable.
    /// </summary>
    public static class EnumerableUtility
    {
        /// <summary>
        ///     Returns an empty array. This can be used when a lot of empty arrays are being created;
        ///     using this method gives the option of not wasting CPU cycles creating lots of empty arrays.
        /// </summary>
        public static T[] EmptyArray<T>()
        {
            return EnumerableUtility<T>.EmptyArray;
        }

        /// <summary>
        ///     This simply returns the parameters you specify in the form of an array.
        /// </summary>
        public static T[] ToEnumerable<T>(params T[] t)
        {
            return t;
        }
    }

    /// <summary>
    ///     Contains internal generic properties (properties for which there is one unique value per type).
    /// </summary>
    internal class EnumerableUtility<T>
    {
        static EnumerableUtility()
        {
            EmptyArray = new T[0];
        }

        public static T[] EmptyArray { get; private set; }
    }
}
