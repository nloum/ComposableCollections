using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadWriteCachedDictionaryWithBuiltInKey<TKey, TValue> : IWriteCachedDictionaryWithBuiltInKey<TKey, TValue>,
        IReadCachedDictionaryWithBuiltInKey<TKey, TValue>
    {
        IReadWriteCachedDictionary<TKey, TValue> AsReadWriteCachedDictionary();
    }
}