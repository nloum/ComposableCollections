using ComposableCollections.Dictionary.Interfaces;
using System;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousDisposableQueryableDictionary<TKey, TValue> : IDisposableQueryableDictionary<TKey, TValue> {
private IComposableDictionary<TKey, TValue> _composableDictionary;
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IDisposable _disposable;
public AnonymousDisposableQueryableDictionary(IComposableDictionary<TKey, TValue> composableDictionary, IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IDisposable disposable) {
_composableDictionary = composableDictionary;
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_disposable = disposable;
}
}
}

