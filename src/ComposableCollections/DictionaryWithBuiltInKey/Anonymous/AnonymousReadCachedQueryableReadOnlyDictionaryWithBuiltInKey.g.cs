﻿using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.DictionaryWithBuiltInKey.Anonymous {
public class AnonymousReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> {
private readonly IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> _readOnlyDictionaryWithBuiltInKey;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IQueryableReadOnlyDictionary<TKey, TValue>> _asQueryableReadOnlyDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IReadCachedQueryableReadOnlyDictionary<TKey, TValue>> _asReadCachedQueryableReadOnlyDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IReadCachedReadOnlyDictionary<TKey, TValue>> _asReadCachedReadOnlyDictionary;
private readonly Func<IQueryable<TValue>> _getValues;
private readonly Action _reloadCache;
public AnonymousReadCachedQueryableReadOnlyDictionaryWithBuiltInKey(IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> readOnlyDictionaryWithBuiltInKey, Func<ComposableCollections.Dictionary.Interfaces.IQueryableReadOnlyDictionary<TKey, TValue>> asQueryableReadOnlyDictionary, Func<ComposableCollections.Dictionary.Interfaces.IReadCachedQueryableReadOnlyDictionary<TKey, TValue>> asReadCachedQueryableReadOnlyDictionary, Func<ComposableCollections.Dictionary.Interfaces.IReadCachedReadOnlyDictionary<TKey, TValue>> asReadCachedReadOnlyDictionary, Func<IQueryable<TValue>> getValues, Action reloadCache) {
_asReadCachedQueryableReadOnlyDictionary = asReadCachedQueryableReadOnlyDictionary;
_reloadCache = reloadCache;
_asReadCachedReadOnlyDictionary = asReadCachedReadOnlyDictionary;
_readOnlyDictionaryWithBuiltInKey = readOnlyDictionaryWithBuiltInKey;
_getValues = getValues;
_asQueryableReadOnlyDictionary = asQueryableReadOnlyDictionary;
}
public virtual TValue this[ TKey key] => _readOnlyDictionaryWithBuiltInKey[ key];
public virtual System.Collections.Generic.IEqualityComparer<TKey> Comparer => _readOnlyDictionaryWithBuiltInKey.Comparer;
public virtual System.Collections.Generic.IEnumerable<TKey> Keys => _readOnlyDictionaryWithBuiltInKey.Keys;
System.Collections.Generic.IEnumerable<TValue> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Values => _readOnlyDictionaryWithBuiltInKey.Values;
IQueryable<TValue> IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Values => _getValues();
public virtual int Count => _readOnlyDictionaryWithBuiltInKey.Count;
public virtual ComposableCollections.Dictionary.Interfaces.IReadCachedQueryableReadOnlyDictionary<TKey, TValue> AsReadCachedQueryableReadOnlyDictionary() {
return _asReadCachedQueryableReadOnlyDictionary();
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
public virtual ComposableCollections.Dictionary.Interfaces.IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary() {
return _asQueryableReadOnlyDictionary();
}
IEnumerator IEnumerable.GetEnumerator() {
return _readOnlyDictionaryWithBuiltInKey.GetEnumerator();}
}
}

