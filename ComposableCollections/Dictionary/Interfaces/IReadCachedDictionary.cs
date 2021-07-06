namespace ComposableCollections.Dictionary.Interfaces
{
    public interface IReadCachedDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>, IReadCachedReadOnlyDictionary<TKey, TValue>
    {
        
    }
}