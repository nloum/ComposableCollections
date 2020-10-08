using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadWriteCachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        IReadCachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>,
        IWriteCachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> AsReadWriteCachedDisposableQueryableDictionary();
    }
}