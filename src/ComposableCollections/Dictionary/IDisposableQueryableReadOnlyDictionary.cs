namespace ComposableCollections.Dictionary
{
    public interface IDisposableQueryableReadOnlyDictionary<TKey, TValue> : IQueryableReadOnlyDictionary<TKey, TValue>,
        IDisposableReadOnlyDictionary<TKey, TValue>
    {
    }
}