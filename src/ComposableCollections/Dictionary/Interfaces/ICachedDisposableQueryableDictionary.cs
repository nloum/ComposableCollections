namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedDisposableQueryableDictionary<TKey, TValue> : ICachedDisposableDictionary<TKey, TValue>, ICachedQueryableDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue> { }
}