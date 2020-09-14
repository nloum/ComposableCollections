using ComposableCollections.Dictionary.Interfaces;
using System;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousDisposableQueryableReadOnlyDictionary<TKey, TValue> : IDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IDisposable _disposable;
public AnonymousDisposableQueryableReadOnlyDictionary(IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IDisposable disposable) {
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_disposable = disposable;
}
}
}

