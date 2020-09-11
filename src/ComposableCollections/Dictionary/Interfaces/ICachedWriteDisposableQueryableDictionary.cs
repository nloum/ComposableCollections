namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedWriteDisposableQueryableDictionary<TKey, TValue> : ICachedWriteDisposableDictionary<TKey, TValue>, ICachedWriteQueryableDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue> { }
}