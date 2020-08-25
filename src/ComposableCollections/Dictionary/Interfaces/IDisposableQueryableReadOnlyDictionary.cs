namespace ComposableCollections.Dictionary
{
    public interface IDisposableQueryableReadOnlyDictionary<TKey, out TValue> : IDisposableReadOnlyDictionary<TKey, TValue>, IQueryableReadOnlyDictionary<TKey, TValue> { }
}