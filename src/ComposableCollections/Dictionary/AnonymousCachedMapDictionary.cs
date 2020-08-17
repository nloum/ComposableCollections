using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousCachedMapDictionary<TKey, TValue, TInnerValue> : CacheMapDictionaryBase<TKey, TValue, TInnerValue> where TValue : class
    {
        private Func<TKey, TValue, TInnerValue> _convert;
        private Func<TKey, TInnerValue, TValue> _convertBack;
        
        public AnonymousCachedMapDictionary(IComposableDictionary<TKey, TInnerValue> innerValues, Func<TKey, TValue, TInnerValue> convert, Func<TKey, TInnerValue, TValue> convertBack, IComposableDictionary<TKey, TValue> cache, bool proactivelyConvertAllValues = false) : base(innerValues, cache, false)
        {
            _convert = convert;
            _convertBack = convertBack;
            
            if (proactivelyConvertAllValues)
            {
                foreach (var innerValue in _innerValues)
                {
                    Convert(innerValue.Key, innerValue.Value);
                }
            }
        }

        protected override TInnerValue StatelessConvert(Func<TKey, TValue, TInnerValue> convert, TKey key, TValue value)
        {
            return _convert(key, value);
        }

        protected override TValue StatelessConvert(Func<TKey, TInnerValue, TValue> convert, TKey key, TInnerValue innerValue)
        {
            return _convertBack(key, innerValue);
        }
    }
}