﻿using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Adapters {
public class ReadWriteCachedQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : QueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>, IReadWriteCachedQueryableDictionary<TKey, TValue> {
private readonly IReadWriteCachedQueryableDictionary<TKey, TSourceValue> _adapted;
public ReadWriteCachedQueryableMappingValuesDictionaryAdapter(IReadWriteCachedQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) : base(adapted, convertTo2, convertTo1) {
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