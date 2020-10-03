using System;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using SimpleMonads;
using System.Linq;
using System.Collections.Generic;using ComposableCollections.Dictionary.Interfaces;using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Adapters {
public class WriteCachedQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>, IWriteCachedQueryableDictionary<TKey, TValue> {
private readonly IWriteCachedQueryableDictionary<TKey, TSourceValue> _adapted;
public WriteCachedQueryableMappingValuesDictionaryAdapter(IWriteCachedQueryableDictionary<TKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public WriteCachedQueryableMappingValuesDictionaryAdapter(IWriteCachedQueryableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public WriteCachedQueryableMappingValuesDictionaryAdapter(IWriteCachedQueryableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;

IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;


public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
