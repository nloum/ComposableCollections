using System;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>, IReadCachedWriteCachedDictionary<TKey, TValue> {
private readonly IReadCachedWriteCachedDictionary<TKey, TSourceValue> _adapted;
public ReadCachedWriteCachedMappingValuesDictionaryAdapter(IReadCachedWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public ReadCachedWriteCachedMappingValuesDictionaryAdapter(IReadCachedWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) : base(adapted, convertTo2, convertTo1) {
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
