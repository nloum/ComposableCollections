using System;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedDisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>, IReadCachedDisposableReadOnlyDictionary<TKey, TValue> {
private readonly IReadCachedDisposableReadOnlyDictionary<TKey, TSourceValue> _adapted;
public ReadCachedDisposableReadOnlyMappingValuesDictionaryAdapter(IReadCachedDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2) : base(adapted, convertTo2) {
_adapted = adapted;}
public ReadCachedDisposableReadOnlyMappingValuesDictionaryAdapter(IReadCachedDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) : base(adapted, convertTo2) {
_adapted = adapted;}
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
