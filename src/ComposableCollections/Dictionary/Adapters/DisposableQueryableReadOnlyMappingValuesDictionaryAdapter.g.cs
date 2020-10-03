using System;
using ComposableCollections.Dictionary.Interfaces;
using System.Linq;
using System.Collections.Generic;
using SimpleMonads;
using ComposableCollections.Dictionary.Interfaces;using System.Collections.Generic;
namespace ComposableCollections.Dictionary.Adapters {
public class DisposableQueryableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>, IDisposableQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> _adapted;
public DisposableQueryableReadOnlyMappingValuesDictionaryAdapter(IDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public DisposableQueryableReadOnlyMappingValuesDictionaryAdapter(IDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) : base(adapted, convertTo2) {
_adapted = adapted;}
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;

System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;


public virtual void Dispose() {
_adapted.Dispose();
}

}
}
