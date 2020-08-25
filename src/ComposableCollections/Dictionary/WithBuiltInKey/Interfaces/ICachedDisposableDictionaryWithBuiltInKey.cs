namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public interface ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        ICachedDisposableDictionary<TKey, TValue> AsCachedDisposableDictionary();
    }
}