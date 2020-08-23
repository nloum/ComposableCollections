using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    /// <summary>
    /// Using two abstract Convert methods, converts an IDictionaryEx{TKey, TInnerValue} to an
    /// IDictionaryEx{TKey, TValue} instance. This will convert objects in the underlying innerValues.
    /// This class assumes that innerValues will not change underneath it.
    /// This class calls the Convert method as rarely as possible.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInnerValue"></typeparam>
    public abstract class CachedReadOnlyMapDictionaryBase<TKey, TValue, TInnerValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        protected readonly IComposableReadOnlyDictionary<TKey, TInnerValue> _innerValues;
        private readonly IComposableDictionary<TKey, TValue> _cache;

        public CachedReadOnlyMapDictionaryBase(IComposableReadOnlyDictionary<TKey, TInnerValue> innerValues, IComposableDictionary<TKey, TValue> cache, bool proactivelyConvertAllValues = false)
        {
            _cache = cache ?? new ConcurrentDictionary<TKey, TValue>();
            _innerValues = innerValues;
            if (proactivelyConvertAllValues)
            {
                foreach (var innerValue in _innerValues)
                {
                    Convert(innerValue.Key, innerValue.Value);
                }
            }
        }

        protected virtual TValue Convert(TKey key, TInnerValue innerValue)
        {
            if (_cache.TryGetValue(key, out var alreadyConvertedValue))
            {
                return alreadyConvertedValue;
            }

            var converted = StatelessConvert(Convert, key, innerValue);
            _cache[key] = converted;
            return converted;
        }

        protected abstract TValue StatelessConvert(Func<TKey, TInnerValue, TValue> convert, TKey key, TInnerValue innerValue);
        
        
        public bool ContainsKey(TKey key)
        {
            return _innerValues.ContainsKey(key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TValue this[TKey key] => TryGetValue(key).Value;

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            var innerValue = _innerValues.TryGetValue(key);
            if (!innerValue.HasValue)
            {
                return Maybe<TValue>.Nothing(() => throw new KeyNotFoundException());
            }

            var result = Convert(key, innerValue.Value);
            return result.ToMaybe();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _innerValues.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, Convert(kvp.Key, kvp.Value))).GetEnumerator();
        }

        public int Count => _innerValues.Count;
        public IEqualityComparer<TKey> Comparer => _innerValues.Comparer;
        public IEnumerable<TKey> Keys => _innerValues.Keys;
        public IEnumerable<TValue> Values => _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value));

    }
}