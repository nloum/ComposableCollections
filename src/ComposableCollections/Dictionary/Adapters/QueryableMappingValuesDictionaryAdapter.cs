using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class QueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> :
        MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>,
        IQueryableDictionary<TKey, TValue>
    {
        private readonly IQueryableDictionary<TKey, TSourceValue> _innerValues;
        private readonly Expression<Func<TSourceValue, TValue>> _convertTo2;

        public QueryableMappingValuesDictionaryAdapter(IQueryableDictionary<TKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) : base(innerValues, convertTo2.Compile(), convertTo1)
        {
            _innerValues = innerValues;
            _convertTo2 = convertTo2;
        }

        public IQueryable<TValue> Values => _innerValues.Values.Select(_convertTo2);
    }
}