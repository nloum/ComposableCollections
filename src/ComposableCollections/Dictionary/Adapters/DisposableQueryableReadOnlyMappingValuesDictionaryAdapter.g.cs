using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters {
public class DisposableQueryableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : QueryableMappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>, IDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> _adapted;
public DisposableQueryableReadOnlyMappingValuesDictionaryAdapter(IDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2) : base(adapted, convertTo2) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
