using System;
using System.Collections.Generic;
using System.Linq;

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
    public abstract class WeakMapDictionaryBase<TKey, TValue, TInnerValue> : MapEnumerableDictionaryBase<TKey, TValue, TInnerValue> where TValue : class
    {
        private readonly IDictionaryEx<TKey, TInnerValue> _innerValues;
        private readonly ConcurrentDictionaryEx<TKey, WeakReference<TValue>> _alreadyConvertedValues = new ConcurrentDictionaryEx<TKey, WeakReference<TValue>>();

        public WeakMapDictionaryBase(IDictionaryEx<TKey, TInnerValue> innerValues, bool proactivelyConvertAllValues) : base(innerValues)
        {
            _innerValues = innerValues;
            if (proactivelyConvertAllValues)
            {
                Convert(_innerValues).ToList();
            }
        }

        protected override IEnumerable<IKeyValuePair<TKey, TInnerValue>> Convert(IEnumerable<IKeyValuePair<TKey, TValue>> values)
        {
            foreach (var value in values)
            {
                if (_innerValues.TryGetValue(value.Key, out var alreadyConvertedValue))
                {
                    yield return Utility.KeyValuePair(value.Key, alreadyConvertedValue);
                }

                yield return Utility.KeyValuePair(value.Key, StatelessConvert(value.Key, value.Value));
            }
        }

        protected abstract TInnerValue StatelessConvert(TKey key, TValue value);
        
        protected override IEnumerable<IKeyValuePair<TKey, TValue>> Convert(IEnumerable<IKeyValuePair<TKey, TInnerValue>> values)
        {
            foreach (var kvp in values)
            {
                var innerValue = kvp.Value;
                var key = kvp.Key;
                if (_alreadyConvertedValues.TryGetValue(key, out var alreadyConvertedValue))
                {
                    if (alreadyConvertedValue.TryGetTarget(out var result))
                    {
                        yield return Utility.KeyValuePair(key, result);
                    }
                }

                var converted = StatelessConvert(key, innerValue);
                _alreadyConvertedValues[key] = new WeakReference<TValue>(converted);
                yield return Utility.KeyValuePair(key, converted);
            }
        }

        protected abstract TValue StatelessConvert(TKey key, TInnerValue innerValue);
    }
}