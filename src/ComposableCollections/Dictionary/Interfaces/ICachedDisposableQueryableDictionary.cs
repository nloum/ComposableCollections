namespace ComposableCollections.Dictionary
{
    public interface ICachedDisposableQueryableDictionary<TKey, TValue> : ICachedDisposableDictionary<TKey, TValue>, ICachedQueryableDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue> { }
}