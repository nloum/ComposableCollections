using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousDisposableQueryableReadOnlyDictionary<TKey, TValue> : IDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IComposableReadOnlyDictionary<TKey, TValue> _composableReadOnlyDictionary;
private readonly Action _dispose;
private readonly Func<IQueryable<TValue>> _getValues;
public AnonymousDisposableQueryableReadOnlyDictionary(IComposableReadOnlyDictionary<TKey, TValue> composableReadOnlyDictionary, Action dispose, Func<IQueryable<TValue>> getValues) {
_getValues = getValues;
_composableReadOnlyDictionary = composableReadOnlyDictionary;
_dispose = dispose;
}
public virtual TValue this[ TKey key] => _composableReadOnlyDictionary[ key];
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _getValues();
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _composableReadOnlyDictionary.Values;
public virtual System.Collections.Generic.IEqualityComparer<TKey> Comparer => _composableReadOnlyDictionary.Comparer;
public virtual System.Collections.Generic.IEnumerable<TKey> Keys => _composableReadOnlyDictionary.Keys;
public virtual int Count => _composableReadOnlyDictionary.Count;
public virtual TValue GetValue( TKey key) {
return _composableReadOnlyDictionary.GetValue( key);
}
public virtual bool ContainsKey( TKey key) {
return _composableReadOnlyDictionary.ContainsKey( key);
}
public virtual IMaybe<TValue> TryGetValue( TKey key) {
return _composableReadOnlyDictionary.TryGetValue( key);
}
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
return _composableReadOnlyDictionary.GetEnumerator();
}
public virtual void Dispose() {
_dispose();
}
IEnumerator IEnumerable.GetEnumerator() {
return _composableReadOnlyDictionary.GetEnumerator();}
}
}

