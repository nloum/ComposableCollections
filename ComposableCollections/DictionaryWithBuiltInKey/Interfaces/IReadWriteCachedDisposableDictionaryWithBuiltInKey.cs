using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadWriteCachedDisposableDictionaryWithBuiltInKey<TKey, TValue> :
        IReadCachedDisposableDictionaryWithBuiltInKey<TKey, TValue>,
        IWriteCachedDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        IReadWriteCachedDisposableDictionary<TKey, TValue> AsReadWriteCachedDisposableDictionary();
    }
}