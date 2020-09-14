namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadCachedDisposableReadOnlyDictionary<TKey, TValue> : IDisposableReadOnlyDictionary<TKey, TValue>, IReadCachedReadOnlyDictionary<TKey, TValue> {
}
}