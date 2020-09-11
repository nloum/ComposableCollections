namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadWriteDictionary<TKey, TValue> : ICachedWriteDictionary<TKey, TValue>,
        ICachedReadDictionary<TKey, TValue>
    {
    }
}