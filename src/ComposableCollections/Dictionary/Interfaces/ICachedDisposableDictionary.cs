namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedDisposableDictionary<TKey, TValue> : ICachedDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue> { }
}