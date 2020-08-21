using System.Collections.Generic;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    /// <summary>
    /// Using an abstract Convert method, converts an IComposableReadOnlyDictionary{TKey, TInnerValue} to an
    /// IComposableReadOnlyDictionary{TKey, TInnerValue} instance. This will convert objects in the underlying innerValues.
    /// This class assumes that innerValues will not change underneath it.
    /// This class calls the Convert method only when Convert hasn't been called for that key yet.
    public abstract class CachedBulkReadOnlyMapDictionaryBase<TKey, TValue, TInnerValue> : BulkReadOnlyMapDictionaryBase<TKey, TValue, TInnerValue>
    {
        private readonly IComposableReadOnlyDictionary<TKey, TInnerValue> _innerValues;
        private readonly IComposableDictionary<TKey, TValue> _cache;

        public CachedBulkReadOnlyMapDictionaryBase(IComposableReadOnlyDictionary<TKey, TInnerValue> innerValues, IComposableDictionary<TKey, TValue> cache, bool proactivelyConvertAllValues) : base(innerValues)
        {
            _innerValues = innerValues;
            _cache = cache ?? new ConcurrentDictionary<TKey, TValue>();
            if (proactivelyConvertAllValues)
            {
                Convert(_innerValues).ToList();
            }
        }

        protected override IEnumerable<IKeyValue<TKey, TValue>> Convert(IEnumerable<IKeyValue<TKey, TInnerValue>> values)
        {
            foreach (var kvp in values)
            {
                var innerValue = kvp.Value;
                var key = kvp.Key;
                if (_cache.TryGetValue(key, out var alreadyConvertedValue))
                {
                    yield return new KeyValue<TKey, TValue>(key, alreadyConvertedValue);
                }

                var converted = StatelessConvert(key, innerValue);
                _cache[key] = converted;
                yield return new KeyValue<TKey, TValue>(key, converted);
            }
        }

        protected abstract TValue StatelessConvert(TKey key, TInnerValue innerValue);
    }
}