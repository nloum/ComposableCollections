namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadWriteDisposableDictionary<TKey, TValue> : ICachedReadDisposableDictionary<TKey, TValue>, ICachedWriteDisposableDictionary<TKey, TValue> {}
}