
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadWriteCachedDictionary<TKey, TValue> : IWriteCachedDictionary<TKey, TValue>, IReadCachedDictionary<TKey, TValue> {
}
}