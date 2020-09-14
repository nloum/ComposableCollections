using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousWriteCachedQueryableDictionary<TKey, TValue> : IWriteCachedQueryableDictionary<TKey, TValue> {
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IWriteCachedDictionary<TKey, TValue> _writeCachedDictionary;
public AnonymousWriteCachedQueryableDictionary(IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IWriteCachedDictionary<TKey, TValue> writeCachedDictionary) {
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_writeCachedDictionary = writeCachedDictionary;
}
}
}

