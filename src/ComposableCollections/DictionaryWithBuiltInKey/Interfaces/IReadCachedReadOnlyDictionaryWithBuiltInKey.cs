using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    /// <summary>
    /// A dictionary that will load items from another (presumably slower, or remote) collection somewhere and cache them
    /// for quick access.
    /// </summary>
    public interface IReadCachedReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        void ReloadCache();
        IReadCachedReadOnlyDictionary<TKey, TValue> AsReadCachedReadOnlyDictionary();
    }
}