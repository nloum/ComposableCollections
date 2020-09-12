using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedWriteDisposableDictionaryWithBuiltInKey<TKey, TValue>, ICachedWriteQueryableDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        IWriteCachedDisposableQueryableDictionary<TKey, TValue> AsCachedDisposableQueryableDictionary();
    }
}