using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class ReadCachedQueryableMappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> :
        MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>,
        IReadCachedQueryableReadOnlyDictionary<TKey, TValue>
    {
        private readonly Func<TKey, TSourceKey> _convertToKey1;
        private readonly IReadCachedQueryableReadOnlyDictionary<TSourceKey, TSourceValue> _innerValues;
        private readonly Expression<Func<TSourceValue, TValue>> _convertTo2;

        public ReadCachedQueryableMappingKeysAndValuesReadOnlyDictionaryAdapter(IReadCachedQueryableReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(innerValues, convertTo2.Compile(), convertToKey2, convertToKey1)
        {
            _convertToKey1 = convertToKey1;
        }

        private static Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> Convert(Func<TSourceKey, TKey> convertToKey2, Expression<Func<TSourceValue, TValue>> convertTo2)
        {
            var compiled = convertTo2.Compile();
            return (key, value) => new KeyValue<TKey, TValue>(convertToKey2(key), compiled(value));
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

        public IQueryable<TValue> Values => _innerValues.Values.Select(_convertTo2);
    }
}