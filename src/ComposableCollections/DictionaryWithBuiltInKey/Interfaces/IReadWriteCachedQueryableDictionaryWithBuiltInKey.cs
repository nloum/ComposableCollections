using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadWriteCachedQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        IReadCachedQueryableDictionaryWithBuiltInKey<TKey, TValue>,
        IWriteCachedQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        IReadWriteCachedQueryableDictionary<TKey, TValue> AsReadWriteCachedQueryableDictionary();
    }
}