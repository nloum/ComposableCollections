using System;
using ComposableCollections.Dictionary.Interfaces;
using System.Linq;
using System.Collections.Generic;
using SimpleMonads;
using ComposableCollections.Dictionary.Interfaces;using System.Collections.Generic;
namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedDisposableQueryableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>, IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> _adapted;
public ReadCachedDisposableQueryableReadOnlyMappingValuesDictionaryAdapter(IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public ReadCachedDisposableQueryableReadOnlyMappingValuesDictionaryAdapter(IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) : base(adapted, convertTo2) {
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
