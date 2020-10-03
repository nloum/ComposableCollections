using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class QueryableMappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue> :
        MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>,
        IQueryableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IQueryableDictionary<TKey, TSourceValue> _innerValues;
        private readonly Expression<Func<TSourceValue, TValue>> _convertTo2;

        public QueryableMappingValuesReadOnlyDictionaryAdapter(IQueryableReadOnlyDictionary<TKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2) : base(innerValues, convertTo2.Compile())
        {
        }

        public IQueryable<TValue> Values => _innerValues.Values.Select(_convertTo2);
    }
}