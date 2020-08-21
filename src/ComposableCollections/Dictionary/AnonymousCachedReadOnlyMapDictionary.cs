using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousCachedReadOnlyMapDictionary<TKey, TValue, TInnerValue> : CacheReadOnlyMapDictionaryBase<TKey, TValue, TInnerValue>
    {
        private Func<TKey, TInnerValue, TValue> _convertBack;
        
        public AnonymousCachedReadOnlyMapDictionary(IComposableReadOnlyDictionary<TKey, TInnerValue> innerValues, Func<TKey, TInnerValue, TValue> convertBack, IComposableDictionary<TKey, TValue> cache, bool proactivelyConvertAllValues = false) : base(innerValues, cache, false)
        {
            _convertBack = convertBack;
            
            if (proactivelyConvertAllValues)
            {
                foreach (var innerValue in _innerValues)
                {
                    Convert(innerValue.Key, innerValue.Value);
                }
            }
        }

        protected override TValue StatelessConvert(Func<TKey, TInnerValue, TValue> convert, TKey key, TInnerValue innerValue)
        {
            return _convertBack(key, innerValue);
        }
    }
}