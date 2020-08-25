namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public interface ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        ICachedQueryableDictionary<TKey, TValue> AsCachedQueryableDictionary();
    }
}