namespace ComposableCollections.Dictionary.Interfaces
{
    public interface IQueryableDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>, IQueryableReadOnlyDictionary<TKey, TValue>
    {
    }
}