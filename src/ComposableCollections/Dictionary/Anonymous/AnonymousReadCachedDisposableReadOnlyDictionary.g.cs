using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ComposableCollections.Dictionary.Anonymous {
public class AnonymousReadCachedDisposableReadOnlyDictionary<TKey, TValue> : IReadCachedDisposableReadOnlyDictionary<TKey, TValue> {
private readonly IComposableReadOnlyDictionary<TKey, TValue> _composableReadOnlyDictionary;
private readonly Action _dispose;
private readonly Action _invalidCache;
private readonly Action<TKey> _invalidCache1;
private readonly Action _reloadCache;
private readonly Action<TKey> _reloadCache1;
public AnonymousReadCachedDisposableReadOnlyDictionary(IComposableReadOnlyDictionary<TKey, TValue> composableReadOnlyDictionary, Action dispose, Action invalidCache, Action<TKey> invalidCache1, Action reloadCache, Action<TKey> reloadCache1) {
_composableReadOnlyDictionary = composableReadOnlyDictionary;
_dispose = dispose;
_reloadCache = reloadCache;
_reloadCache1 = reloadCache1;
_invalidCache = invalidCache;
_invalidCache1 = invalidCache1;
}
public virtual TValue this[ TKey key] => _composableReadOnlyDictionary[ key];
public virtual System.Collections.Generic.IEqualityComparer<TKey> Comparer => _composableReadOnlyDictionary.Comparer;
public virtual System.Collections.Generic.IEnumerable<TKey> Keys => _composableReadOnlyDictionary.Keys;
public virtual System.Collections.Generic.IEnumerable<TValue> Values => _composableReadOnlyDictionary.Values;
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

