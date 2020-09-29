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
public class ReadCachedDisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue> : DictionaryGetOrDefaultDecorator<TKey, TValue>, IReadCachedDisposableQueryableDictionary<TKey, TValue> {
private readonly IReadCachedDisposableQueryableDictionary<TKey, TValue> _adapted;
public ReadCachedDisposableQueryableGetOrDefaultDictionaryDecorator(IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public ReadCachedDisposableQueryableGetOrDefaultDictionaryDecorator(IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public ReadCachedDisposableQueryableGetOrDefaultDictionaryDecorator(IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;

IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;


public virtual void Dispose() {
_adapted.Dispose();
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
