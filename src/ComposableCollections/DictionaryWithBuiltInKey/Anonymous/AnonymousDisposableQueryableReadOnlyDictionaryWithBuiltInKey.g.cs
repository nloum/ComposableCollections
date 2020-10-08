using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.DictionaryWithBuiltInKey.Anonymous {
public class AnonymousDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> {
private readonly IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> _readOnlyDictionaryWithBuiltInKey;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IDisposableQueryableReadOnlyDictionary<TKey, TValue>> _asDisposableQueryableReadOnlyDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue>> _asDisposableReadOnlyDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IQueryableReadOnlyDictionary<TKey, TValue>> _asQueryableReadOnlyDictionary;
private readonly Action _dispose;
private readonly Func<IQueryable<TValue>> _getValues;
public AnonymousDisposableQueryableReadOnlyDictionaryWithBuiltInKey(IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> readOnlyDictionaryWithBuiltInKey, Func<ComposableCollections.Dictionary.Interfaces.IDisposableQueryableReadOnlyDictionary<TKey, TValue>> asDisposableQueryableReadOnlyDictionary, Func<ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue>> asDisposableReadOnlyDictionary, Func<ComposableCollections.Dictionary.Interfaces.IQueryableReadOnlyDictionary<TKey, TValue>> asQueryableReadOnlyDictionary, Action dispose, Func<IQueryable<TValue>> getValues) {
_asDisposableQueryableReadOnlyDictionary = asDisposableQueryableReadOnlyDictionary;
_asDisposableReadOnlyDictionary = asDisposableReadOnlyDictionary;
_readOnlyDictionaryWithBuiltInKey = readOnlyDictionaryWithBuiltInKey;
_dispose = dispose;
_getValues = getValues;
_asQueryableReadOnlyDictionary = asQueryableReadOnlyDictionary;
}
public virtual TValue this[ TKey key] => _readOnlyDictionaryWithBuiltInKey[ key];
public virtual System.Collections.Generic.IEqualityComparer<TKey> Comparer => _readOnlyDictionaryWithBuiltInKey.Comparer;
public virtual System.Collections.Generic.IEnumerable<TKey> Keys => _readOnlyDictionaryWithBuiltInKey.Keys;
System.Collections.Generic.IEnumerable<TValue> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Values => _readOnlyDictionaryWithBuiltInKey.Values;
IQueryable<TValue> IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Values => _getValues();
public virtual int Count => _readOnlyDictionaryWithBuiltInKey.Count;
public virtual ComposableCollections.Dictionary.Interfaces.IDisposableQueryableReadOnlyDictionary<TKey, TValue> AsDisposableQueryableReadOnlyDictionary() {
return _asDisposableQueryableReadOnlyDictionary();
}
public virtual ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary() {
return _asDisposableReadOnlyDictionary();
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
public virtual void Dispose() {
_dispose();
}
public virtual ComposableCollections.Dictionary.Interfaces.IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary() {
return _asQueryableReadOnlyDictionary();
}
IEnumerator IEnumerable.GetEnumerator() {
return _readOnlyDictionaryWithBuiltInKey.GetEnumerator();}
}
}

