using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedWriteQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedWriteDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        ICachedWriteQueryableDictionary<TKey, TValue> AsCachedQueryableDictionary();
    }
}