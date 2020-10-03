using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters {
public class DisposableQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : QueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>, IDisposableQueryableDictionary<TKey, TValue> {
private readonly IDisposableQueryableDictionary<TKey, TSourceValue> _adapted;
public DisposableQueryableMappingValuesDictionaryAdapter(IDisposableQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
