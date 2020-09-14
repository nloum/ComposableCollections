using ComposableCollections.Dictionary.Interfaces;
using System;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadWriteCachedDisposableQueryableDictionary<TKey, TValue> : IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> {
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IDisposable _disposable;
private IWriteCachedDictionary<TKey, TValue> _writeCachedDictionary;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadWriteCachedDisposableQueryableDictionary(IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IDisposable disposable, IWriteCachedDictionary<TKey, TValue> writeCachedDictionary, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_disposable = disposable;
_writeCachedDictionary = writeCachedDictionary;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
}
}

