using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        ICachedDisposableDictionary<TKey, TValue> AsCachedDisposableDictionary();
    }
}