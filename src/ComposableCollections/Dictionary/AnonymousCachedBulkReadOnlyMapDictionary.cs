using System;
using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    public class AnonymousCachedBulkReadOnlyMapDictionary<TKey, TValue, TInnerValue> : CachedBulkReadOnlyMapDictionaryBase<TKey, TValue, TInnerValue>
    {
        private Func<TKey, TInnerValue, TValue> _convertBack;
        private Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> _bulkConvertBack;
        
        public AnonymousCachedBulkReadOnlyMapDictionary(IComposableReadOnlyDictionary<TKey, TInnerValue> innerValues, Func<TKey, TInnerValue, TValue> convertBack, IComposableDictionary<TKey, TValue> cache = null, bool proactivelyConvertAllValues = false) : base(innerValues, cache, proactivelyConvertAllValues)
        {
            _convertBack = convertBack;
        }

        public AnonymousCachedBulkReadOnlyMapDictionary(IComposableReadOnlyDictionary<TKey, TInnerValue> innerValues, Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> convertBack, IComposableDictionary<TKey, TValue> cache = null, bool proactivelyConvertAllValues = false) : base(innerValues, cache, proactivelyConvertAllValues)
        {
            _bulkConvertBack = convertBack;
        }

        protected override IEnumerable<IKeyValue<TKey, TValue>> Convert(IEnumerable<IKeyValue<TKey, TInnerValue>> values)
        {
            if (_bulkConvertBack != null)
            {
                return _bulkConvertBack(values);
            }

            return base.Convert(values);
        }

        protected override TValue StatelessConvert(TKey key, TInnerValue innerValue)
        {
            return _convertBack(key, innerValue);
        }
    }
}