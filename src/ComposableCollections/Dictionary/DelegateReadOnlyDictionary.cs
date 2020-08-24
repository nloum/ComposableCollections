using System;
using System.Collections.Generic;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class DelegateReadOnlyDictionary<TKey, TValue> : ReadOnlyDictionaryBase<TKey, TValue>
    {
        private IComposableReadOnlyDictionary<TKey, TValue> _source;

        public DelegateReadOnlyDictionary(IComposableReadOnlyDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        protected DelegateReadOnlyDictionary()
        {
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> wrapped)
        {
            _source = wrapped;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            var result = _source.TryGetValue(key);
            value = result.ValueOrDefault;
            return result.HasValue;
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _source.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count
        {
            get
            {
                if (_source == null)
                {
                    throw new InvalidOperationException("Must call SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _source.Count;
            }
        }

        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;
        public override IEnumerable<TKey> Keys
        {
            get
            {
                if (_source == null)
                {
                    throw new InvalidOperationException("Must call SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _source.Keys;
            }
        }

        public override IEnumerable<TValue> Values
        {
            get
            {
                if (_source == null)
                {
                    throw new InvalidOperationException("Must call SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _source.Values;
            }
        }
    }
}