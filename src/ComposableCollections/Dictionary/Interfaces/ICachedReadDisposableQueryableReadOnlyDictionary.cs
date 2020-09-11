namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadDisposableQueryableReadOnlyDictionary<TKey, out TValue> : ICachedReadDisposableReadOnlyDictionary<TKey, TValue>, ICachedReadQueryableReadOnlyDictionary<TKey, TValue> { }
}