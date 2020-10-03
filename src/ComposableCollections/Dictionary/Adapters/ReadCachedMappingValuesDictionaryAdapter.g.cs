using System;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>, IReadCachedDictionary<TKey, TValue> {
private readonly IReadCachedDictionary<TKey, TSourceValue> _adapted;
public ReadCachedMappingValuesDictionaryAdapter(IReadCachedDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public ReadCachedMappingValuesDictionaryAdapter(IReadCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) : base(adapted, convertTo2, convertTo1) {
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
