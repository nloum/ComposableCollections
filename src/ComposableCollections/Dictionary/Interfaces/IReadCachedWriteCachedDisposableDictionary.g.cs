
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadCachedWriteCachedDisposableDictionary<TKey, TValue> : IWriteCachedDisposableDictionary<TKey, TValue>, IReadCachedDisposableDictionary<TKey, TValue>, IReadCachedWriteCachedDictionary<TKey, TValue> {
}
}