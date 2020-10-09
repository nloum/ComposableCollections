
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadWriteCachedQueryableDictionary<TKey, TValue> : IWriteCachedQueryableDictionary<TKey, TValue>, IReadCachedQueryableDictionary<TKey, TValue>, IReadWriteCachedDictionary<TKey, TValue> {
}
}