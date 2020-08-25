namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedQueryableDictionary<TKey, TValue> : ICachedDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue> { }
}