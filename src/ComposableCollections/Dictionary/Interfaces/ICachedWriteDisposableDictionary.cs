namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedWriteDisposableDictionary<TKey, TValue> : ICachedWriteDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue> { }
}