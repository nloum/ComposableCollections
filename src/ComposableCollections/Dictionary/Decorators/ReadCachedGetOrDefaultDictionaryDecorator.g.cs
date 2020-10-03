using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Decorators {
public class ReadCachedGetOrDefaultDictionaryDecorator<TKey, TValue> : DictionaryGetOrDefaultDecorator<TKey, TValue>, IReadCachedDictionary<TKey, TValue> {
private readonly IReadCachedDictionary<TKey, TValue> _adapted;
public ReadCachedGetOrDefaultDictionaryDecorator(IReadCachedDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public ReadCachedGetOrDefaultDictionaryDecorator(IReadCachedDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public ReadCachedGetOrDefaultDictionaryDecorator(IReadCachedDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public virtual void ReloadCache() {
_adapted.ReloadCache();
}

public virtual void ReloadCache( TKey key) {
_adapted.ReloadCache( key);
}

public virtual void InvalidCache() {
_adapted.InvalidCache();
}

public virtual void InvalidCache( TKey key) {
_adapted.InvalidCache( key);
}

}
}
