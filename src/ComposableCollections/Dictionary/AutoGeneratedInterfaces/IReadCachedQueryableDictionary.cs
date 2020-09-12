namespace ComposableCollections.Dictionary.Interfaces {
public interface IReadCachedQueryableDictionary<TKey, TValue> : IQueryableDictionary<TKey, TValue>, IReadCachedDictionary<TKey, TValue>, IReadCachedQueryableReadOnlyDictionary<TKey, TValue> {
}
}