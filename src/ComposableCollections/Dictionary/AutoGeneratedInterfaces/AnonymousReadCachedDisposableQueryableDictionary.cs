using ComposableCollections.Dictionary.Interfaces;
using System;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadCachedDisposableQueryableDictionary<TKey, TValue> : IReadCachedDisposableQueryableDictionary<TKey, TValue> {
private IComposableDictionary<TKey, TValue> _composableDictionary;
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IDisposable _disposable;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadCachedDisposableQueryableDictionary(IComposableDictionary<TKey, TValue> composableDictionary, IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IDisposable disposable, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_composableDictionary = composableDictionary;
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_disposable = disposable;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
}
}

