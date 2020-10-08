
namespace ComposableCollections.Dictionary.Interfaces {
public interface IWriteCachedDisposableQueryableDictionary<TKey, TValue> : IDisposableQueryableDictionary<TKey, TValue>, IWriteCachedQueryableDictionary<TKey, TValue>, IWriteCachedDisposableDictionary<TKey, TValue> {
}
}