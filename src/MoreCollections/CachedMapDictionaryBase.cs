using System.ComponentModel;
using System.Linq;

namespace MoreCollections
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
    public abstract class CachedMapDictionaryBase<TKey, TValue, TInnerValue> : MapDictionaryBase<TKey, TValue, TInnerValue> where TValue : class
    {
        private readonly IDictionaryEx<TKey, TInnerValue> _innerValues;
        private readonly ConcurrentDictionaryEx<TKey, TValue> _alreadyConvertedValues = new ConcurrentDictionaryEx<TKey, TValue>();

        public CachedMapDictionaryBase(IDictionaryEx<TKey, TInnerValue> innerValues, bool proactivelyConvertAllValues) : base(innerValues)
        {
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

            return StatelessConvert(key, value);
        }

        protected abstract TInnerValue StatelessConvert(TKey key, TValue value);
        
        protected override TValue Convert(TKey key, TInnerValue innerValue)
        {
            if (_alreadyConvertedValues.TryGetValue(key, out var alreadyConvertedValue))
            {
                return alreadyConvertedValue;
            }

            var converted = StatelessConvert(key, innerValue);
            _alreadyConvertedValues[key] = converted;
            return converted;
        }

        protected abstract TValue StatelessConvert(TKey key, TInnerValue innerValue);
    }
}