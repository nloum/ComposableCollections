using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedWriteQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedWriteDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        IWriteCachedQueryableDictionary<TKey, TValue> AsCachedQueryableDictionary();
    }
}