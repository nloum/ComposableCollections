using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class QueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> :
        MappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>,
        IQueryableDictionary<TKey, TValue>
    {
        private readonly IQueryableDictionary<TSourceKey, TSourceValue> _innerValues;
        private readonly Expression<Func<TSourceValue, TValue>> _convertTo2;

        public QueryableMappingKeysAndValuesDictionaryAdapter(IQueryableDictionary<TSourceKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(innerValues, convertTo2.Compile(), convertTo1, convertToKey2, convertToKey1)
        {
            _innerValues = innerValues;
            _convertTo2 = convertTo2;
        }

        public new IQueryable<TValue> Values => _innerValues.Values.Select(_convertTo2);
    }
}