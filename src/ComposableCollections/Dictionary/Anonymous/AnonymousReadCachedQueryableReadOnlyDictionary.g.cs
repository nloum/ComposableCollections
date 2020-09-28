using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.Dictionary.Anonymous {
public class AnonymousReadCachedQueryableReadOnlyDictionary<TKey, TValue> : IReadCachedQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IComposableReadOnlyDictionary<TKey, TValue> _composableReadOnlyDictionary;
private readonly Func<IQueryable<TValue>> _getValues;
private readonly Action _invalidCache;
private readonly Action<TKey> _invalidCache1;
private readonly Action _reloadCache;
private readonly Action<TKey> _reloadCache1;
public AnonymousReadCachedQueryableReadOnlyDictionary(IComposableReadOnlyDictionary<TKey, TValue> composableReadOnlyDictionary, Func<IQueryable<TValue>> getValues, Action invalidCache, Action<TKey> invalidCache1, Action reloadCache, Action<TKey> reloadCache1) {
_getValues = getValues;
_composableReadOnlyDictionary = composableReadOnlyDictionary;
_reloadCache = reloadCache;
_reloadCache1 = reloadCache1;
_invalidCache = invalidCache;
_invalidCache1 = invalidCache1;
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
public virtual void ReloadCache() {
_reloadCache();
}
public virtual void ReloadCache( TKey key) {
_reloadCache1( key);
}
public virtual void InvalidCache() {
_invalidCache();
}
public virtual void InvalidCache( TKey key) {
_invalidCache1( key);
}
IEnumerator IEnumerable.GetEnumerator() {
return _composableReadOnlyDictionary.GetEnumerator();}
}
}

