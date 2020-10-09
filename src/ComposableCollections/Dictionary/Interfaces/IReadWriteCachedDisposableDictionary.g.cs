
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadWriteCachedDisposableDictionary<TKey, TValue> : IWriteCachedDisposableDictionary<TKey, TValue>, IReadCachedDisposableDictionary<TKey, TValue>, IReadWriteCachedDictionary<TKey, TValue> {
}
}