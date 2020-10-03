using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class QueryableMappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> :
        MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>,
        IQueryableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IQueryableDictionary<TSourceKey, TSourceValue> _innerValues;
        private readonly Expression<Func<TSourceValue, TValue>> _convertTo2;

        public QueryableMappingKeysAndValuesReadOnlyDictionaryAdapter(IQueryableReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(innerValues, convertTo2.Compile(), convertToKey2, convertToKey1)
        {
        }

        private static Func<TSourceKey,TSourceValue,IKeyValue<TKey,TValue>> Convert(Func<TSourceKey,TKey> convertToKey2, Expression<Func<TSourceValue,TValue>> convertTo2)
        {
            var compiled = convertTo2.Compile();
            return (key, value) => new KeyValue<TKey, TValue>(convertToKey2(key), compiled(value));
        }

        public IQueryable<TValue> Values => _innerValues.Values.Select(_convertTo2);
    }
}