using System;
using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    public class AnonymousCachedBulkMapDictionary<TKey, TValue, TInnerValue> : CachedBulkMapDictionaryBase<TKey, TValue, TInnerValue>
    {
        private Func<TKey, TValue, TInnerValue> _convert;
        private Func<TKey, TInnerValue, TValue> _convertBack;
        private Func<IEnumerable<IKeyValue<TKey, TValue>>, IEnumerable<IKeyValue<TKey, TInnerValue>>> _bulkConvert;
        private Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> _bulkConvertBack;
        
        public AnonymousCachedBulkMapDictionary(IComposableDictionary<TKey, TInnerValue> innerValues, Func<TKey, TValue, TInnerValue> convert, Func<TKey, TInnerValue, TValue> convertBack, IComposableDictionary<TKey, TValue> cache = null, bool proactivelyConvertAllValues = false) : base(innerValues, cache, proactivelyConvertAllValues)
        {
            _convert = convert;
            _convertBack = convertBack;
        }

        public AnonymousCachedBulkMapDictionary(IComposableDictionary<TKey, TInnerValue> innerValues, Func<IEnumerable<IKeyValue<TKey, TValue>>, IEnumerable<IKeyValue<TKey, TInnerValue>>> convert, Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> convertBack, IComposableDictionary<TKey, TValue> cache = null, bool proactivelyConvertAllValues = false) : base(innerValues, cache, proactivelyConvertAllValues)
        {
            _bulkConvert = convert;
            _bulkConvertBack = convertBack;
        }

        protected override IEnumerable<IKeyValue<TKey, TInnerValue>> Convert(IEnumerable<IKeyValue<TKey, TValue>> values)
        {
            if (_bulkConvert != null)
            {
                return _bulkConvert(values);
            }

            return base.Convert(values);
        }

        protected override IEnumerable<IKeyValue<TKey, TValue>> Convert(IEnumerable<IKeyValue<TKey, TInnerValue>> values)
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