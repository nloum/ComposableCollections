using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedDisposableQueryableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : QueryableMappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>, IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> _adapted;
public ReadCachedDisposableQueryableReadOnlyMappingValuesDictionaryAdapter(IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2) : base(adapted, convertTo2) {
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
