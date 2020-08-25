namespace ComposableCollections.Dictionary
{
    public interface IQueryableDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>, IQueryableReadOnlyDictionary<TKey, TValue>
    {
    }
}