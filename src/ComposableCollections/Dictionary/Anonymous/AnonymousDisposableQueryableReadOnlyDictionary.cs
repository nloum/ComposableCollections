using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousDisposableQueryableReadOnlyDictionary<TKey, TValue> : IDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IDisposable _disposable;
public AnonymousDisposableQueryableReadOnlyDictionary(IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IDisposable disposable) {
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_disposable = disposable;
}
System.Collections.Generic.IEqualityComparer<TKey> IComposableReadOnlyDictionary<TKey, TValue>.Comparer => _queryableReadOnlyDictionary.Comparer;
TValue IComposableReadOnlyDictionary<TKey, TValue>.GetValue( TKey key) {
return _queryableReadOnlyDictionary.GetValue( key);
}
TValue IComposableReadOnlyDictionary<TKey, TValue>.this[ TKey key] => _queryableReadOnlyDictionary[ key];
System.Collections.Generic.IEnumerable<TKey> IComposableReadOnlyDictionary<TKey, TValue>.Keys => _queryableReadOnlyDictionary.Keys;
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _queryableReadOnlyDictionary.Values;
bool IComposableReadOnlyDictionary<TKey, TValue>.ContainsKey( TKey key) {
return _queryableReadOnlyDictionary.ContainsKey( key);
}
IMaybe<TValue> IComposableReadOnlyDictionary<TKey, TValue>.TryGetValue( TKey key) {
return _queryableReadOnlyDictionary.TryGetValue( key);
}
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _queryableReadOnlyDictionary.Values;
int IReadOnlyCollection<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.Count => _queryableReadOnlyDictionary.Count;
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
return _queryableReadOnlyDictionary.GetEnumerator();
}
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _queryableReadOnlyDictionary.GetEnumerator();
}
void IDisposable.Dispose() {
_disposable.Dispose();
}
}
}

