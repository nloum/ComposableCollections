using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollections
{
    public class DelegateReadOnlyDictionaryEx<TKey, TValue> : ReadOnlyDictionaryBaseEx<TKey, TValue>
    {
        private IReadOnlyDictionaryEx<TKey, TValue>? _wrapped;

        public DelegateReadOnlyDictionaryEx(IReadOnlyDictionaryEx<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        protected DelegateReadOnlyDictionaryEx()
        {
        }

        protected virtual void SetWrapped(IReadOnlyDictionaryEx<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            var result = _wrapped.TryGetValue(key);
            value = result.ValueOrDefault;
            return result.HasValue;
        }

        public override IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.Select(kvp => Utility.KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count
        {
            get
            {
                if (_wrapped == null)
                {
                    throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _wrapped.Count;
            }
        }

        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;
        public override IEnumerable<TKey> Keys
        {
            get
            {
                if (_wrapped == null)
                {
                    throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _wrapped.Keys;
            }
        }

        public override IEnumerable<TValue> Values
        {
            get
            {
                if (_wrapped == null)
                {
                    throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _wrapped.Values;
            }
        }
    }
}