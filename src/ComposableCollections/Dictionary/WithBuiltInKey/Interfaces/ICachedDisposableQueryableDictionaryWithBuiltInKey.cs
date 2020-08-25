using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>, ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        ICachedDisposableQueryableDictionary<TKey, TValue> AsCachedDisposableQueryableDictionary();
    }
}