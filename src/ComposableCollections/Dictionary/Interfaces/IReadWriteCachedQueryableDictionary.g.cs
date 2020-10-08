
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadWriteCachedQueryableDictionary<TKey, TValue> : IQueryableDictionary<TKey, TValue>, IReadWriteCachedDictionary<TKey, TValue> {
}
}