namespace ComposableCollections.Dictionary
{
    public interface IDisposableQueryableDictionary<TKey, TValue> : IDisposableDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue>, IDisposableQueryableReadOnlyDictionary<TKey, TValue> { }
}