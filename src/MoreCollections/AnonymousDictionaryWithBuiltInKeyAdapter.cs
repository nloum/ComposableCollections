using System;

namespace MoreCollections
{
    public class AnonymousDictionaryWithBuiltInKeyAdapter<TKey, TValue> : DictionaryWithBuiltInKeyAdapter<TKey, TValue>
    {
        private readonly Func<TValue, TKey> _getKey;
        
        public AnonymousDictionaryWithBuiltInKeyAdapter(IDictionaryEx<TKey, TValue> wrapped, Func<TValue, TKey> getKey) : base(wrapped)
        {
            _getKey = getKey;
        }

        public AnonymousDictionaryWithBuiltInKeyAdapter(Func<TValue, TKey> getKey)
        {
            _getKey = getKey;
        }

        public override TKey GetKey(TValue value)
        {
            return _getKey(value);
        }
    }
}