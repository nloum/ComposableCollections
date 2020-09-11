namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadDisposableDictionary<TKey, TValue> : ICachedReadDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>, ICachedReadDisposableReadOnlyDictionary<TKey, TValue> { }
}