
namespace ComposableCollections.Dictionary.Interfaces {
public interface ReadWriteCachedDisposable<TKey, TValue> : IWriteCachedDisposableDictionary<TKey, TValue>, IReadCachedDisposableDictionary<TKey, TValue>, ReadWriteCached<TKey, TValue> {
}
}