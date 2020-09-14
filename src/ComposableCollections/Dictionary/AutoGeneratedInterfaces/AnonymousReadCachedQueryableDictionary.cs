using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadCachedQueryableDictionary<TKey, TValue> : IReadCachedQueryableDictionary<TKey, TValue> {
private IComposableDictionary<TKey, TValue> _composableDictionary;
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadCachedQueryableDictionary(IComposableDictionary<TKey, TValue> composableDictionary, IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_composableDictionary = composableDictionary;
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
}
}

