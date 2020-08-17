using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousCacheMapDictionary<TKey, TValue, TInnerValue> : CacheMapDictionaryBase<TKey, TValue, TInnerValue> where TValue : class
    {
        private Func<Func<TKey, TValue, TInnerValue>, TKey, TValue, TInnerValue> _convert;
        private Func<Func<TKey, TInnerValue, TValue>, TKey, TInnerValue, TValue> _convertBack;
        
        public AnonymousCacheMapDictionary(IComposableDictionary<TKey, TInnerValue> innerValues, Func<Func<TKey, TValue, TInnerValue>, TKey, TValue, TInnerValue> convert, Func<Func<TKey, TInnerValue, TValue>, TKey, TInnerValue, TValue> convertBack, bool proactivelyConvertAllValues) : base(innerValues, proactivelyConvertAllValues)
        {
            _convert = convert;
            _convertBack = convertBack;
        }

        protected override TInnerValue StatelessConvert(Func<TKey, TValue, TInnerValue> convert, TKey key, TValue value)
        {
            return _convert(convert, key, value);
        }

        protected override TValue StatelessConvert(Func<TKey, TInnerValue, TValue> convert, TKey key, TInnerValue innerValue)
        {
            return _convertBack(convert, key, innerValue);
        }
    }
}