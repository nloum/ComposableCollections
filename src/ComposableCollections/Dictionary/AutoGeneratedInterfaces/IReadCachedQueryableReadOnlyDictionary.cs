namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadCachedQueryableReadOnlyDictionary<TKey, TValue> : IQueryableReadOnlyDictionary<TKey, TValue>, IReadCachedReadOnlyDictionary<TKey, TValue> {
}
}