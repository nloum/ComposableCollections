using System.Linq;
using System;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using SimpleMonads;
using ComposableCollections.Dictionary.Interfaces;using System.Collections.Generic;
namespace ComposableCollections.Dictionary.Adapters {
public class QueryableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>, IQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IQueryableReadOnlyDictionary<TKey, TSourceValue> _adapted;
public QueryableReadOnlyMappingValuesDictionaryAdapter(IQueryableReadOnlyDictionary<TKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public QueryableReadOnlyMappingValuesDictionaryAdapter(IQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) : base(adapted, convertTo2) {
_adapted = adapted;}
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;

System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;


}
}
