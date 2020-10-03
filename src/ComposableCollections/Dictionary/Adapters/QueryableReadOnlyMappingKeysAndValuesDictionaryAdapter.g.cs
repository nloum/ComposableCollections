using System.Linq;
using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;
using ComposableCollections.Dictionary.Interfaces;using System.Collections.Generic;
namespace ComposableCollections.Dictionary.Adapters {
public class QueryableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, IQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IQueryableReadOnlyDictionary<TSourceKey, TSourceValue> _adapted;
public QueryableReadOnlyMappingKeysAndValuesDictionaryAdapter(IQueryableReadOnlyDictionary<TSourceKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public QueryableReadOnlyMappingKeysAndValuesDictionaryAdapter(IQueryableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertToKey2, convertToKey1) {
_adapted = adapted;}
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;

System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;


}
}
