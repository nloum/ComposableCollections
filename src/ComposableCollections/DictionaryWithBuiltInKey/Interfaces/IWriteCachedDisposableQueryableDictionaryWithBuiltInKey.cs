using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IWriteCachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        IWriteCachedDisposableDictionaryWithBuiltInKey<TKey, TValue>, IWriteCachedQueryableDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        IWriteCachedDisposableQueryableDictionary<TKey, TValue> AsWriteCachedDisposableQueryableDictionary();
    }
}