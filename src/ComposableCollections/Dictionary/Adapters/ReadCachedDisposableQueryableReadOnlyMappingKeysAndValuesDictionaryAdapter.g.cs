﻿using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;
using ComposableCollections.Dictionary.Interfaces;using System.Collections.Generic;
namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedDisposableQueryableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IReadCachedDisposableQueryableReadOnlyDictionary<TSourceKey, TSourceValue> _adapted;
public ReadCachedDisposableQueryableReadOnlyMappingKeysAndValuesDictionaryAdapter(IReadCachedDisposableQueryableReadOnlyDictionary<TSourceKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public ReadCachedDisposableQueryableReadOnlyMappingKeysAndValuesDictionaryAdapter(IReadCachedDisposableQueryableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertToKey2, convertToKey1) {
_adapted = adapted;}
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;

System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;


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
