
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadCachedDisposableDictionary<TKey, TValue> : IDisposableDictionary<TKey, TValue>, IReadCachedDictionary<TKey, TValue>, IReadCachedDisposableReadOnlyDictionary<TKey, TValue> {
}
}