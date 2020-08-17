using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousMapComposableDictionary<TKey, TValue, TInnerValue> : MapComposableDictionaryBase<TKey, TValue, TInnerValue> where TValue : class
    {
        private Func<TKey, TValue, TInnerValue> _convert;
        private Func<TKey, TInnerValue, TValue> _convertBack;
        
        public AnonymousMapComposableDictionary(IComposableDictionary<TKey, TInnerValue> innerValues, Func<TKey, TValue, TInnerValue> convert, Func<TKey, TInnerValue, TValue> convertBack) : base(innerValues)
        {
            _convert = convert;
            _convertBack = convertBack;
        }

        protected override TInnerValue Convert(TKey key, TValue value)
        {
            return _convert(key, value);
        }

        protected override TValue Convert(TKey key, TInnerValue innerValue)
        {
            return _convertBack(key, innerValue);
        }
    }
}