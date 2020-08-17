using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousDictionaryWithBuiltInKey<TKey, TValue> : DictionaryWithBuiltInKeyAdapter<TKey, TValue>, IDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly Func<TValue, TKey> _getKey;

        public AnonymousDictionaryWithBuiltInKey(IComposableDictionary<TKey, TValue> wrapped, Func<TValue, TKey> key) : base(wrapped)
        {
            _getKey = key;
        }

        public override TKey GetKey(TValue value)
        {
            return _getKey(value);
        }
    }
}