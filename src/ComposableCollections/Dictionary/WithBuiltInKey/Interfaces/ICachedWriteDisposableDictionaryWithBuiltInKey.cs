using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedWriteDisposableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedWriteDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        ICachedWriteDisposableDictionary<TKey, TValue> AsCachedDisposableDictionary();
    }
}