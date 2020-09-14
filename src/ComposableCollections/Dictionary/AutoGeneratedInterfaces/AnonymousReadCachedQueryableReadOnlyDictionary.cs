using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadCachedQueryableReadOnlyDictionary<TKey, TValue> : IReadCachedQueryableReadOnlyDictionary<TKey, TValue> {
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadCachedQueryableReadOnlyDictionary(IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
}
}

