
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadWriteCachedDisposableDictionary<TKey, TValue> : IDisposableDictionary<TKey, TValue>, IReadWriteCachedDictionary<TKey, TValue> {
}
}