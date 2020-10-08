
namespace ComposableCollections.Dictionary.Interfaces {
public interface ReadWriteCachedQueryable<TKey, TValue> : IWriteCachedQueryableDictionary<TKey, TValue>, IReadCachedQueryableDictionary<TKey, TValue>, ReadWriteCached<TKey, TValue> {
}
}