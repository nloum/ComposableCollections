using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> :
        IReadCachedReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
        IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IReadCachedQueryableReadOnlyDictionary<TKey, TValue> AsReadCachedQueryableReadOnlyDictionary();
    }
}