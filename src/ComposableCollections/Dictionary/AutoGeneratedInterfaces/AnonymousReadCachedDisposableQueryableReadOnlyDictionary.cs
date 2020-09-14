using ComposableCollections.Dictionary.Interfaces;
using System;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> : IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IDisposable _disposable;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadCachedDisposableQueryableReadOnlyDictionary(IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IDisposable disposable, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_disposable = disposable;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
}
}

