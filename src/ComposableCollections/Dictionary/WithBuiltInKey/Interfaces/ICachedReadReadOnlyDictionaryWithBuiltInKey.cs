using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    /// <summary>
    /// A dictionary that will load items from another (presumably slower, or remote) collection somewhere and cache them
    /// for quick access.
    /// </summary>
    public interface ICachedReadReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        void ReloadCache();
    }
}