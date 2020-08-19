namespace ComposableCollections.Dictionary
{
    public interface IDisposableDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>, IDisposableReadOnlyDictionary<TKey, TValue>
    {
    }
}