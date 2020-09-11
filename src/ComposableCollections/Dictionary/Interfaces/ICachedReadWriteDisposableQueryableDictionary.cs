namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadWriteDisposableQueryableDictionary<TKey, TValue> : ICachedReadDisposableQueryableDictionary<TKey, TValue>, ICachedWriteDisposableQueryableDictionary<TKey, TValue> { }
}