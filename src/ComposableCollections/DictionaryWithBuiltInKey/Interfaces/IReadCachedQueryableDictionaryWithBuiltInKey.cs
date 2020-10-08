using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadCachedQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        IReadCachedDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>,
        IReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IReadCachedQueryableDictionary<TKey, TValue> AsReadCachedQueryableDictionary();
    }
}