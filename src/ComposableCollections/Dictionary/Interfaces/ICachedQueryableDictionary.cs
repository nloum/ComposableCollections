namespace ComposableCollections.Dictionary
{
    public interface ICachedQueryableDictionary<TKey, TValue> : ICachedDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue> { }
}