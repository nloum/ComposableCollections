using System;

namespace MoreCollections
{
    public class AnonymousDictionaryWithBuiltInKey<TKey, TValue> : DictionaryWithBuiltInKeyExBase<TKey, TValue>, IDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly Func<TValue, TKey> _getKey;

        public AnonymousDictionaryWithBuiltInKey(IDictionaryEx<TKey, TValue> wrapped, Func<TValue, TKey> key) : base(wrapped)
        {
            _getKey = key;
        }

        public override TKey GetKey(TValue value)
        {
            return _getKey(value);
        }
    }
}