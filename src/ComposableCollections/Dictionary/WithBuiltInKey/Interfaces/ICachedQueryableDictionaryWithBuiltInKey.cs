using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        ICachedQueryableDictionary<TKey, TValue> AsCachedQueryableDictionary();
    }
}