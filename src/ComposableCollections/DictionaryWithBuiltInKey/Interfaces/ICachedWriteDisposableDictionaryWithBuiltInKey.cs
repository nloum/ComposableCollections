using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedWriteDisposableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedWriteDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        IWriteCachedDisposableDictionary<TKey, TValue> AsCachedDisposableDictionary();
    }
}