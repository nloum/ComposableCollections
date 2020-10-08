
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadCachedWriteCachedDictionary<TKey, TValue> : IWriteCachedDictionary<TKey, TValue>, IReadCachedDictionary<TKey, TValue> {
}
}