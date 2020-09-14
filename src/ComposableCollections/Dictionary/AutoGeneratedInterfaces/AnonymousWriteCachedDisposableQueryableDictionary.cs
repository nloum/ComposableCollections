using ComposableCollections.Dictionary.Interfaces;
using System;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousWriteCachedDisposableQueryableDictionary<TKey, TValue> : IWriteCachedDisposableQueryableDictionary<TKey, TValue> {
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IDisposable _disposable;
private IWriteCachedDictionary<TKey, TValue> _writeCachedDictionary;
public AnonymousWriteCachedDisposableQueryableDictionary(IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IDisposable disposable, IWriteCachedDictionary<TKey, TValue> writeCachedDictionary) {
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_disposable = disposable;
_writeCachedDictionary = writeCachedDictionary;
}
}
}

