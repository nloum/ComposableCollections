using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IWriteCachedDisposableDictionaryWithBuiltInKey<TKey, TValue> :
        IWriteCachedDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        IWriteCachedDisposableDictionary<TKey, TValue> AsWriteCachedDisposableDictionary();
    }
}