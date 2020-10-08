
namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> : IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IReadCachedQueryableReadOnlyDictionary<TKey, TValue>, IReadCachedDisposableReadOnlyDictionary<TKey, TValue> {
}
}