using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Adapters {
public class WriteCachedQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : QueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>, IWriteCachedQueryableDictionary<TKey, TValue> {
private readonly IWriteCachedQueryableDictionary<TKey, TSourceValue> _adapted;
public WriteCachedQueryableMappingValuesDictionaryAdapter(IWriteCachedQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
