using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters {
public class DisposableQueryableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : QueryableMappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, IDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IDisposableQueryableReadOnlyDictionary<TSourceKey, TSourceValue> _adapted;
public DisposableQueryableReadOnlyMappingKeysAndValuesDictionaryAdapter(IDisposableQueryableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertToKey2, convertToKey1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
