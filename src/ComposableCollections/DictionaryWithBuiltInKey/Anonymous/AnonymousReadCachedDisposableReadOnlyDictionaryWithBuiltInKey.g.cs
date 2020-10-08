using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ComposableCollections.DictionaryWithBuiltInKey.Anonymous {
public class AnonymousReadCachedDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadCachedDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> {
private readonly IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> _readOnlyDictionaryWithBuiltInKey;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue>> _asDisposableReadOnlyDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IReadCachedDisposableReadOnlyDictionary<TKey, TValue>> _asReadCachedDisposableReadOnlyDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IReadCachedReadOnlyDictionary<TKey, TValue>> _asReadCachedReadOnlyDictionary;
private readonly Action _dispose;
private readonly Action _reloadCache;
public AnonymousReadCachedDisposableReadOnlyDictionaryWithBuiltInKey(IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> readOnlyDictionaryWithBuiltInKey, Func<ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue>> asDisposableReadOnlyDictionary, Func<ComposableCollections.Dictionary.Interfaces.IReadCachedDisposableReadOnlyDictionary<TKey, TValue>> asReadCachedDisposableReadOnlyDictionary, Func<ComposableCollections.Dictionary.Interfaces.IReadCachedReadOnlyDictionary<TKey, TValue>> asReadCachedReadOnlyDictionary, Action dispose, Action reloadCache) {
_asReadCachedDisposableReadOnlyDictionary = asReadCachedDisposableReadOnlyDictionary;
_reloadCache = reloadCache;
_asReadCachedReadOnlyDictionary = asReadCachedReadOnlyDictionary;
_readOnlyDictionaryWithBuiltInKey = readOnlyDictionaryWithBuiltInKey;
_asDisposableReadOnlyDictionary = asDisposableReadOnlyDictionary;
_dispose = dispose;
}
public virtual TValue this[ TKey key] => _readOnlyDictionaryWithBuiltInKey[ key];
public virtual System.Collections.Generic.IEqualityComparer<TKey> Comparer => _readOnlyDictionaryWithBuiltInKey.Comparer;
public virtual System.Collections.Generic.IEnumerable<TKey> Keys => _readOnlyDictionaryWithBuiltInKey.Keys;
public virtual System.Collections.Generic.IEnumerable<TValue> Values => _readOnlyDictionaryWithBuiltInKey.Values;
public virtual int Count => _readOnlyDictionaryWithBuiltInKey.Count;
public virtual ComposableCollections.Dictionary.Interfaces.IReadCachedDisposableReadOnlyDictionary<TKey, TValue> AsReadCachedDisposableReadOnlyDictionary() {
return _asReadCachedDisposableReadOnlyDictionary();
}
public virtual void ReloadCache() {
_reloadCache();
}
public virtual ComposableCollections.Dictionary.Interfaces.IReadCachedReadOnlyDictionary<TKey, TValue> AsReadCachedReadOnlyDictionary() {
return _asReadCachedReadOnlyDictionary();
}
public virtual ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> AsComposableReadOnlyDictionary() {
return _readOnlyDictionaryWithBuiltInKey.AsComposableReadOnlyDictionary();
}
public virtual TKey GetKey( TValue value) {
return _readOnlyDictionaryWithBuiltInKey.GetKey( value);
}
public virtual TValue GetValue( TKey key) {
return _readOnlyDictionaryWithBuiltInKey.GetValue( key);
}
public virtual bool ContainsKey( TKey key) {
return _readOnlyDictionaryWithBuiltInKey.ContainsKey( key);
}
public virtual IMaybe<TValue> TryGetValue( TKey key) {
return _readOnlyDictionaryWithBuiltInKey.TryGetValue( key);
}
System.Collections.Generic.IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() {
return _readOnlyDictionaryWithBuiltInKey.GetEnumerator();
}
public virtual ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary() {
return _asDisposableReadOnlyDictionary();
}
public virtual void Dispose() {
_dispose();
}
IEnumerator IEnumerable.GetEnumerator() {
return _readOnlyDictionaryWithBuiltInKey.GetEnumerator();}
}
}

