using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Decorators {
public class ReadCachedWriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue> : DictionaryGetOrRefreshDecorator<TKey, TValue>, IReadCachedWriteCachedDictionary<TKey, TValue> {
private readonly IReadCachedWriteCachedDictionary<TKey, TValue> _adapted;
public ReadCachedWriteCachedGetOrRefreshDictionaryDecorator(IReadCachedWriteCachedDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public ReadCachedWriteCachedGetOrRefreshDictionaryDecorator(IReadCachedWriteCachedDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public ReadCachedWriteCachedGetOrRefreshDictionaryDecorator(IReadCachedWriteCachedDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public virtual void FlushCache() {
_adapted.FlushCache();
}

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
