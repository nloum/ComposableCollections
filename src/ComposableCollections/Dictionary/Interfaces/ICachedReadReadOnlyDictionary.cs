using System.Collections.Generic;

namespace ComposableCollections.Dictionary.Interfaces
{
    /// <summary>
    /// A dictionary that will load items from another (presumably slower, or remote) collection somewhere and cache them
    /// for quick access.
    /// </summary>
    public interface ICachedReadReadOnlyDictionary<TKey, out TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        void ReloadCache();
    }
}