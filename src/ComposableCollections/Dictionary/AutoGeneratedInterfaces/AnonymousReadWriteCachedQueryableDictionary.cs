using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadWriteCachedQueryableDictionary<TKey, TValue> : IReadWriteCachedQueryableDictionary<TKey, TValue> {
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IWriteCachedDictionary<TKey, TValue> _writeCachedDictionary;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadWriteCachedQueryableDictionary(IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IWriteCachedDictionary<TKey, TValue> writeCachedDictionary, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_writeCachedDictionary = writeCachedDictionary;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
}
}

