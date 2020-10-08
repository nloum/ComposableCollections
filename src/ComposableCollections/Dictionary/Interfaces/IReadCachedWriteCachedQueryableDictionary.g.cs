
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadCachedWriteCachedQueryableDictionary<TKey, TValue> : IWriteCachedQueryableDictionary<TKey, TValue>, IReadCachedQueryableDictionary<TKey, TValue>, IReadCachedWriteCachedDictionary<TKey, TValue> {
}
}