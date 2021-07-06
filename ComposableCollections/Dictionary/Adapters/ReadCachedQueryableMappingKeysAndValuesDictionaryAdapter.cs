using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class ReadCachedQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> :
        MappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>,
        IReadCachedQueryableDictionary<TKey, TValue>
    {
        private readonly IReadCachedQueryableDictionary<TSourceKey, TSourceValue> _innerValues;
        private readonly Expression<Func<TSourceValue, TValue>> _convertTo2;
        private readonly Func<TKey, TSourceKey> _convertToKey1;

        public ReadCachedQueryableMappingKeysAndValuesDictionaryAdapter(IReadCachedQueryableDictionary<TSourceKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(innerValues, convertTo2.Compile(), convertTo1, convertToKey2, convertToKey1)
        {
            _innerValues = innerValues;
            _convertTo2 = convertTo2;
            _convertToKey1 = convertToKey1;
        }

        public void ReloadCache()
        {
            _innerValues.ReloadCache();
        }

        public void ReloadCache(TKey key)
        {
            _innerValues.ReloadCache(_convertToKey1(key));
        }

        public void InvalidCache()
        {
            _innerValues.InvalidCache();
        }

        public void InvalidCache(TKey key)
        {
            _innerValues.InvalidCache(_convertToKey1(key));
        }

        public new IQueryable<TValue> Values => _innerValues.Values.Select(_convertTo2);
    }
}