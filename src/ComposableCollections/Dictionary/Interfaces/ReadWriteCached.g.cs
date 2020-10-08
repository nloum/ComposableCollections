
namespace ComposableCollections.Dictionary.Interfaces {
public interface ReadWriteCached<TKey, TValue> : IWriteCachedDictionary<TKey, TValue>, IReadCachedDictionary<TKey, TValue> {
}
}