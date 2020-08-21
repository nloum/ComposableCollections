using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    /// <summary>
    /// Using an abstract Convert method, converts an IComposableReadOnlyDictionary{TKey, TInnerValue} to an
    /// IComposableReadOnlyDictionary{TKey, TValue} instance. This will lazily convert objects in the underlying innerValues.
    /// This class works whether innerValues changes underneath it or not.
    /// </summary>
    public abstract class BulkReadOnlyMapDictionaryBase<TKey, TValue, TInnerValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IComposableReadOnlyDictionary<TKey, TInnerValue> _innerValues;

        protected BulkReadOnlyMapDictionaryBase(IComposableReadOnlyDictionary<TKey, TInnerValue> innerValues)
        {
            _innerValues = innerValues;
        }

        protected abstract IEnumerable<IKeyValue<TKey, TValue>> Convert(
            IEnumerable<IKeyValue<TKey, TInnerValue>> innerValues);

        public bool ContainsKey(TKey key)
        {
            return _innerValues.ContainsKey(key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TValue this[TKey key]
        {
            get
            {
                return TryGetValue(key).Value;
            }
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            var innerValue = _innerValues.TryGetValue(key);
            if (!innerValue.HasValue)
            {
                return Maybe<TValue>.Nothing(() => throw new KeyNotFoundException());
            }

            var result = Convert(new[] { new KeyValue<TKey, TInnerValue>(key, innerValue.Value) }).First().Value;
            return result.ToMaybe();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return Convert(_innerValues).GetEnumerator();
        }

        public int Count => _innerValues.Count;
        public IEqualityComparer<TKey> Comparer => _innerValues.Comparer;
        public IEnumerable<TKey> Keys => _innerValues.Keys;
        public IEnumerable<TValue> Values => this.AsEnumerable().Select(x => x.Value);
    }
}