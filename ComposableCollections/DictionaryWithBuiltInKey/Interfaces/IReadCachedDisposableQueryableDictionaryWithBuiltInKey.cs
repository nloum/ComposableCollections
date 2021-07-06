using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadCachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        IReadCachedDisposableDictionaryWithBuiltInKey<TKey, TValue>,
        IReadCachedQueryableDictionaryWithBuiltInKey<TKey, TValue>,
        IReadCachedDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IReadCachedDisposableQueryableDictionary<TKey, TValue> AsReadCachedDisposableQueryableDictionary();
    }
}