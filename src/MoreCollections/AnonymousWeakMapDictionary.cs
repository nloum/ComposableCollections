using System;

namespace MoreCollections
{
    public class AnonymousWeakMapDictionary<TKey, TValue, TInnerValue> : WeakMapDictionaryBase<TKey, TValue, TInnerValue> where TValue : class
    {
        private Func<TKey, TValue, TInnerValue> _convert;
        private Func<TKey, TInnerValue, TValue> _convertBack;
        
        public AnonymousWeakMapDictionary(IDictionaryEx<TKey, TInnerValue> innerValues, Func<TKey, TValue, TInnerValue> convert, Func<TKey, TInnerValue, TValue> convertBack, bool proactivelyConvertAllValues) : base(innerValues, proactivelyConvertAllValues)
        {
            _convert = convert;
            _convertBack = convertBack;
        }

        protected override TInnerValue StatelessConvert(TKey key, TValue value)
        {
            return _convert(key, value);
        }

        protected override TValue StatelessConvert(TKey key, TInnerValue innerValue)
        {
            return _convertBack(key, innerValue);
        }
    }
}