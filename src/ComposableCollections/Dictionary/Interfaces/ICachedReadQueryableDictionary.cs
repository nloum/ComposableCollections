namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadQueryableDictionary<TKey, TValue> : ICachedReadDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue>, ICachedReadQueryableReadOnlyDictionary<TKey, TValue> { }
}