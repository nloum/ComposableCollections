namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadQueryableReadOnlyDictionary<TKey, out TValue> : ICachedReadReadOnlyDictionary<TKey, TValue>, IQueryableReadOnlyDictionary<TKey, TValue> { }
}