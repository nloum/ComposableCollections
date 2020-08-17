using System;

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
    public abstract class CacheMapDictionaryBase<TKey, TValue, TInnerValue> : MapDictionaryBase<TKey, TValue, TInnerValue> where TValue : class
    {
        protected readonly IComposableDictionary<TKey, TInnerValue> _innerValues;
        private readonly IComposableDictionary<TKey, TValue> _cache;

        public CacheMapDictionaryBase(IComposableDictionary<TKey, TInnerValue> innerValues, IComposableDictionary<TKey, TValue> cache, bool proactivelyConvertAllValues = false) : base(innerValues)
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

        protected override TInnerValue Convert(TKey key, TValue value)
        {
            if (_innerValues.TryGetValue(key, out var alreadyConvertedValue))
            {
                return alreadyConvertedValue;
            }

            return StatelessConvert(Convert, key, value);
        }

        protected abstract TInnerValue StatelessConvert(Func<TKey, TValue, TInnerValue> convert, TKey key, TValue value);
        
        protected override TValue Convert(TKey key, TInnerValue innerValue)
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
    }
}