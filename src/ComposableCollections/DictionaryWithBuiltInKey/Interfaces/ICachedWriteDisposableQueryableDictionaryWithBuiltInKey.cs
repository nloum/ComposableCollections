using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedWriteDisposableDictionaryWithBuiltInKey<TKey, TValue>, ICachedWriteQueryableDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        IWriteCachedDisposableQueryableDictionary<TKey, TValue> AsCachedDisposableQueryableDictionary();
    }
}