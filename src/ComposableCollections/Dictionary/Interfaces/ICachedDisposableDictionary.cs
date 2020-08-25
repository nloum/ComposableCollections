namespace ComposableCollections.Dictionary
{
    public interface ICachedDisposableDictionary<TKey, TValue> : ICachedDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue> { }
}