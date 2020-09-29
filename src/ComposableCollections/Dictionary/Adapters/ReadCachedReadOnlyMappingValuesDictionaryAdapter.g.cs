using System.Collections.Generic;
using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>, IReadCachedReadOnlyDictionary<TKey, TValue> {
private readonly IReadCachedReadOnlyDictionary<TKey, TSourceValue> _adapted;
public ReadCachedReadOnlyMappingValuesDictionaryAdapter(IReadCachedReadOnlyDictionary<TKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public ReadCachedReadOnlyMappingValuesDictionaryAdapter(IReadCachedReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) : base(adapted, convertTo2) {
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
