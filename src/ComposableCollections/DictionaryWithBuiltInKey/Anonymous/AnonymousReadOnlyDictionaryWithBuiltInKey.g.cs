using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ComposableCollections.DictionaryWithBuiltInKey.Anonymous {
public class AnonymousReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> {
private readonly IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> _readOnlyDictionaryWithBuiltInKey;
public AnonymousReadOnlyDictionaryWithBuiltInKey(IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> readOnlyDictionaryWithBuiltInKey) {
_readOnlyDictionaryWithBuiltInKey = readOnlyDictionaryWithBuiltInKey;
}
public virtual TValue this[ TKey key] => _readOnlyDictionaryWithBuiltInKey[ key];
public virtual System.Collections.Generic.IEqualityComparer<TKey> Comparer => _readOnlyDictionaryWithBuiltInKey.Comparer;
public virtual System.Collections.Generic.IEnumerable<TKey> Keys => _readOnlyDictionaryWithBuiltInKey.Keys;
public virtual System.Collections.Generic.IEnumerable<TValue> Values => _readOnlyDictionaryWithBuiltInKey.Values;
public virtual int Count => _readOnlyDictionaryWithBuiltInKey.Count;
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
IEnumerator IEnumerable.GetEnumerator() {
return _readOnlyDictionaryWithBuiltInKey.GetEnumerator();}
}
}

