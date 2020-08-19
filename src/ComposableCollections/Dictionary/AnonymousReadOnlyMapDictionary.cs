using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousReadOnlyMapDictionary<TKey, TValue, TInnerValue> : MapReadOnlyDictionaryBase<TKey, TValue, TInnerValue> where TValue : class
    {
        private Func<TKey, TInnerValue, TValue> _convertBack;
        
        public AnonymousReadOnlyMapDictionary(IComposableReadOnlyDictionary<TKey, TInnerValue> innerValues, Func<TKey, TInnerValue, TValue> convertBack) : base(innerValues)
        {
            _convertBack = convertBack;
        }

        protected override TValue Convert(TKey key, TInnerValue innerValue)
        {
            return _convertBack(key, innerValue);
        }
    }
}