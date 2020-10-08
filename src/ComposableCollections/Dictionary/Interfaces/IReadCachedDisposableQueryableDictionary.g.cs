
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadCachedDisposableQueryableDictionary<TKey, TValue> : IDisposableQueryableDictionary<TKey, TValue>, IReadCachedQueryableDictionary<TKey, TValue>, IReadCachedDisposableDictionary<TKey, TValue>, IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> {
}
}