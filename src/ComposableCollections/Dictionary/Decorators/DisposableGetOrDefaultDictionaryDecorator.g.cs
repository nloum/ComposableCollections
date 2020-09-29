using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Decorators {
public class DisposableGetOrDefaultDictionaryDecorator<TKey, TValue> : DictionaryGetOrDefaultDecorator<TKey, TValue>, IDisposableDictionary<TKey, TValue> {
private readonly IDisposableDictionary<TKey, TValue> _adapted;
public DisposableGetOrDefaultDictionaryDecorator(IDisposableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public DisposableGetOrDefaultDictionaryDecorator(IDisposableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public DisposableGetOrDefaultDictionaryDecorator(IDisposableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
