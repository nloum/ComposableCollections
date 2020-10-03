using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Decorators {
public class ReadCachedGetOrRefreshDictionaryDecorator<TKey, TValue> : DictionaryGetOrRefreshDecorator<TKey, TValue>, IReadCachedDictionary<TKey, TValue> {
private readonly IReadCachedDictionary<TKey, TValue> _adapted;
public ReadCachedGetOrRefreshDictionaryDecorator(IReadCachedDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public ReadCachedGetOrRefreshDictionaryDecorator(IReadCachedDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public ReadCachedGetOrRefreshDictionaryDecorator(IReadCachedDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
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
