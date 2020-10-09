
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> : IWriteCachedDisposableQueryableDictionary<TKey, TValue>, IReadCachedDisposableQueryableDictionary<TKey, TValue>, IReadWriteCachedQueryableDictionary<TKey, TValue>, IReadWriteCachedDisposableDictionary<TKey, TValue> {
}
}