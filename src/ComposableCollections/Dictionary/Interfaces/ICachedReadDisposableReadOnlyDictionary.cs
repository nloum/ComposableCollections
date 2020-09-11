namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadDisposableReadOnlyDictionary<TKey, out TValue> : ICachedReadReadOnlyDictionary<TKey, TValue>, IDisposableReadOnlyDictionary<TKey, TValue> { }
}