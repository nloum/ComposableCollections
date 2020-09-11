namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadDisposableQueryableDictionary<TKey, TValue> : ICachedReadDisposableDictionary<TKey, TValue>, ICachedReadQueryableDictionary<TKey, TValue>, ICachedReadDisposableQueryableReadOnlyDictionary<TKey, TValue> { }
}