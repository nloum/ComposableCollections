using System;

namespace MoreCollections
{
    /// <summary>
    /// Using two abstract Convert methods, converts an IDictionaryEx{TKey, TInnerValue} to an
    /// IDictionaryEx{TKey, TInnerValue} instance. This will convert objects in the underlying innerValues.
    /// This class assumes that innerValues will not change underneath it.
    /// This class calls the Convert method only when Convert hasn't been called for that key yet, or it was called
    /// for that key but the previously converted value has been garbage collected.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInnerValue"></typeparam>
    public abstract class WeakMapDictionaryBase<TKey, TValue, TInnerValue> : MapDictionaryBase<TKey, TValue, TInnerValue> where TValue : class
    {
        private readonly IDictionaryEx<TKey, TInnerValue> _innerValues;
        private readonly ConcurrentDictionaryEx<TKey, WeakReference<TValue>> _alreadyConvertedValues = new ConcurrentDictionaryEx<TKey, WeakReference<TValue>>();

        public WeakMapDictionaryBase(IDictionaryEx<TKey, TInnerValue> innerValues, bool proactivelyConvertAllValues) : base(innerValues)
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

            return StatelessConvert(Convert, key, value);
        }

        protected abstract TInnerValue StatelessConvert(Func<TKey, TValue, TInnerValue> convert, TKey key, TValue value);
        
        protected override TValue Convert(TKey key, TInnerValue innerValue)
        {
            if (_alreadyConvertedValues.TryGetValue(key, out var alreadyConvertedValue))
            {
                if (alreadyConvertedValue.TryGetTarget(out var result))
                {
                    return result;
                }
            }

            var converted = StatelessConvert(Convert, key, innerValue);
            _alreadyConvertedValues[key] = new WeakReference<TValue>(converted);
            return converted;
        }

        protected abstract TValue StatelessConvert(Func<TKey, TInnerValue, TValue> convert, TKey key, TInnerValue innerValue);
    }
}