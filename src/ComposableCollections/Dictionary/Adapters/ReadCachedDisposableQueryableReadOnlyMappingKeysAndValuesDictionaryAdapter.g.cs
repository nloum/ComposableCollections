using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedDisposableQueryableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : ReadCachedQueryableMappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IReadCachedDisposableQueryableReadOnlyDictionary<TSourceKey, TSourceValue> _adapted;
public ReadCachedDisposableQueryableReadOnlyMappingKeysAndValuesDictionaryAdapter(IReadCachedDisposableQueryableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertToKey2, convertToKey1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
