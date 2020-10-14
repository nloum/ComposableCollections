using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ComposableCollections.DictionaryWithBuiltInKey.Decorators {
public class DisposableReadOnlyDictionaryWithBuiltInKeyDecoratorBase<TKey, TValue> : IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> {
private readonly IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> _decoratedObject;
public DisposableReadOnlyDictionaryWithBuiltInKeyDecoratorBase(IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> decoratedObject) {
_decoratedObject = decoratedObject;
}
void IDisposable.Dispose() {
_decoratedObject.Dispose();
}
System.Collections.Generic.IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() {
return _decoratedObject.GetEnumerator();
}
ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.AsComposableReadOnlyDictionary() {
return _decoratedObject.AsComposableReadOnlyDictionary();
}
TKey IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.GetKey( TValue value) {
return _decoratedObject.GetKey( value);
}
System.Collections.Generic.IEqualityComparer<TKey> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Comparer => _decoratedObject.Comparer;
TValue IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.GetValue( TKey key) {
return _decoratedObject.GetValue( key);
}
TValue IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.this[ TKey key] => _decoratedObject[ key];
System.Collections.Generic.IEnumerable<TKey> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Keys => _decoratedObject.Keys;
System.Collections.Generic.IEnumerable<TValue> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Values => _decoratedObject.Values;
bool IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.ContainsKey( TKey key) {
return _decoratedObject.ContainsKey( key);
}
IMaybe<TValue> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.TryGetValue( TKey key) {
return _decoratedObject.TryGetValue( key);
}
ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue> IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.AsDisposableReadOnlyDictionary() {
return _decoratedObject.AsDisposableReadOnlyDictionary();
}
int IReadOnlyCollection<TValue>.Count => _decoratedObject.Count;
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _decoratedObject.GetEnumerator();
}
}
}

