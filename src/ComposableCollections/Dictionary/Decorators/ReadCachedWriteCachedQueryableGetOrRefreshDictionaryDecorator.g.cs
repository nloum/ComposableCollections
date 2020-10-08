using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
using System.Linq;
using System.Collections.Generic;using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Decorators {
public class ReadCachedWriteCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue> : DictionaryGetOrRefreshDecorator<TKey, TValue>, IReadCachedWriteCachedQueryableDictionary<TKey, TValue> {
private readonly IReadCachedWriteCachedQueryableDictionary<TKey, TValue> _adapted;
public ReadCachedWriteCachedQueryableGetOrRefreshDictionaryDecorator(IReadCachedWriteCachedQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public ReadCachedWriteCachedQueryableGetOrRefreshDictionaryDecorator(IReadCachedWriteCachedQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public ReadCachedWriteCachedQueryableGetOrRefreshDictionaryDecorator(IReadCachedWriteCachedQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;

IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;


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
