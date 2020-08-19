using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue> : ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>
    {
        private readonly Func<TValue, TKey> _getKey;
        
        public AnonymousReadOnlyDictionaryWithBuiltInKeyAdapter(IComposableReadOnlyDictionary<TKey, TValue> wrapped, Func<TValue, TKey> getKey) : base(wrapped)
        {
            _getKey = getKey;
        }

        public AnonymousReadOnlyDictionaryWithBuiltInKeyAdapter(Func<TValue, TKey> getKey)
        {
            _getKey = getKey;
        }

        public override TKey GetKey(TValue value)
        {
            return _getKey(value);
        }
    }
}