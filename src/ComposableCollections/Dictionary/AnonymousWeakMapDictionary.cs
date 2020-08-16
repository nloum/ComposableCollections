using System;
using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    public class AnonymousWeakMapDictionary<TKey, TValue, TInnerValue> : WeakMapDictionaryBase<TKey, TValue, TInnerValue> where TValue : class
    {
        private Func<TKey, TValue, TInnerValue> _convert;
        private Func<TKey, TInnerValue, TValue> _convertBack;
        private Func<IEnumerable<IKeyValuePair<TKey, TValue>>, IEnumerable<IKeyValuePair<TKey, TInnerValue>>> _bulkConvert;
        private Func<IEnumerable<IKeyValuePair<TKey, TInnerValue>>, IEnumerable<IKeyValuePair<TKey, TValue>>> _bulkConvertBack;


        public AnonymousWeakMapDictionary(IDictionaryEx<TKey, TInnerValue> innerValues, Func<TKey, TValue, TInnerValue> convert, Func<TKey, TInnerValue, TValue> convertBack, bool proactivelyConvertAllValues) : base(innerValues, proactivelyConvertAllValues)
        {
            _convert = convert;
            _convertBack = convertBack;
        }

        public AnonymousWeakMapDictionary(IDictionaryEx<TKey, TInnerValue> innerValues, Func<IEnumerable<IKeyValuePair<TKey, TValue>>, IEnumerable<IKeyValuePair<TKey, TInnerValue>>> convert, Func<IEnumerable<IKeyValuePair<TKey, TInnerValue>>, IEnumerable<IKeyValuePair<TKey, TValue>>> convertBack, bool proactivelyConvertAllValues) : base(innerValues, proactivelyConvertAllValues)
        {
            _bulkConvert = convert;
            _bulkConvertBack = convertBack;
        }

        protected override IEnumerable<IKeyValuePair<TKey, TInnerValue>> Convert(IEnumerable<IKeyValuePair<TKey, TValue>> values)
        {
            if (_bulkConvert != null)
            {
                return _bulkConvert(values);
            }

            return base.Convert(values);
        }

        protected override IEnumerable<IKeyValuePair<TKey, TValue>> Convert(IEnumerable<IKeyValuePair<TKey, TInnerValue>> values)
        {
            if (_bulkConvertBack != null)
            {
                return _bulkConvertBack(values);
            }

            return base.Convert(values);
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