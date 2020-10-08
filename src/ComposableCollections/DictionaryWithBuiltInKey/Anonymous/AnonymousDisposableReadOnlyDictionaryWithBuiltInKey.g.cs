using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ComposableCollections.DictionaryWithBuiltInKey.Anonymous {
public class AnonymousDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> {
private readonly IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> _readOnlyDictionaryWithBuiltInKey;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue>> _asDisposableReadOnlyDictionary;
private readonly Action _dispose;
public AnonymousDisposableReadOnlyDictionaryWithBuiltInKey(IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> readOnlyDictionaryWithBuiltInKey, Func<ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue>> asDisposableReadOnlyDictionary, Action dispose) {
_asDisposableReadOnlyDictionary = asDisposableReadOnlyDictionary;
_readOnlyDictionaryWithBuiltInKey = readOnlyDictionaryWithBuiltInKey;
_dispose = dispose;
}
public virtual TValue this[ TKey key] => _readOnlyDictionaryWithBuiltInKey[ key];
public virtual System.Collections.Generic.IEqualityComparer<TKey> Comparer => _readOnlyDictionaryWithBuiltInKey.Comparer;
public virtual System.Collections.Generic.IEnumerable<TKey> Keys => _readOnlyDictionaryWithBuiltInKey.Keys;
public virtual System.Collections.Generic.IEnumerable<TValue> Values => _readOnlyDictionaryWithBuiltInKey.Values;
public virtual int Count => _readOnlyDictionaryWithBuiltInKey.Count;
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
IEnumerator IEnumerable.GetEnumerator() {
return _readOnlyDictionaryWithBuiltInKey.GetEnumerator();}
}
}

