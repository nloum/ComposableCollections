using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadCachedDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> :
        IReadCachedDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
        IReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue>
            AsReadCachedDisposableQueryableReadOnlyDictionary();
    }
}