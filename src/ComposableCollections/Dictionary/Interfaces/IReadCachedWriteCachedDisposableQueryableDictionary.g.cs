
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> : IWriteCachedDisposableQueryableDictionary<TKey, TValue>, IReadCachedDisposableQueryableDictionary<TKey, TValue>, IReadCachedWriteCachedQueryableDictionary<TKey, TValue>, IReadCachedWriteCachedDisposableDictionary<TKey, TValue> {
}
}