
namespace ComposableCollections.Dictionary.Interfaces {
public interface ReadWriteCachedDisposableQueryable<TKey, TValue> : IWriteCachedDisposableQueryableDictionary<TKey, TValue>, IReadCachedDisposableQueryableDictionary<TKey, TValue>, ReadWriteCachedQueryable<TKey, TValue>, ReadWriteCachedDisposable<TKey, TValue> {
}
}