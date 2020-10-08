using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IWriteCachedQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        IWriteCachedDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        IWriteCachedQueryableDictionary<TKey, TValue> AsWriteCachedQueryableDictionary();
    }
}