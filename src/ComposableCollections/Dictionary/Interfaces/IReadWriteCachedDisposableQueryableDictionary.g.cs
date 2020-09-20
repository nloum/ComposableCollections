namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> : IDisposableQueryableDictionary<TKey, TValue>, IReadWriteCachedQueryableDictionary<TKey, TValue>, IReadWriteCachedDisposableDictionary<TKey, TValue> {
}
}