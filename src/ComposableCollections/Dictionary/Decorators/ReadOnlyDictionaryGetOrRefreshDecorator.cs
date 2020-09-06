using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Decorators
{
    public class ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        private IComposableReadOnlyDictionary<TKey, TValue> _source;
        private RefreshValue<TKey, TValue> _refreshValue;

        public ReadOnlyDictionaryGetOrRefreshDecorator(IComposableReadOnlyDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue)
        {
            _source = source;
            _refreshValue = refreshValue;
        }

        public ReadOnlyDictionaryGetOrRefreshDecorator(IComposableReadOnlyDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue)
        {
            _source = source;
            _refreshValue = (TKey key, TValue value, out TValue refreshedValue) =>
            {
                refreshedValue = refreshValue(key, value);
                return true;
            };
        }

        protected ReadOnlyDictionaryGetOrRefreshDecorator()
        {
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> getDefaultValue)
        {
            _source = source;
            _refreshValue = getDefaultValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var maybeValue = _source.TryGetValue(key);
            if (maybeValue.HasValue)
            {
                if (_refreshValue(key, maybeValue.Value, out value))
                {
                    return true;
                }
                else
                {
                    value = maybeValue.Value;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            if (TryGetValue(key, out var value))
            {
                return value.ToMaybe();
            }
            
            return Maybe<TValue>.Nothing();
        }

        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var value);
        }
        
        public TValue GetValue(TKey key)
        {
            return this[key];
        }

        public TValue this[TKey key]
        {
            get 
            {
                if (TryGetValue(key, out var value))
                {
                    return value;
                }
                
                throw new KeyNotFoundException();
            }
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        public int Count => _source.Count;

        public IEqualityComparer<TKey> Comparer => _source.Comparer;

        public IEnumerable<TKey> Keys => _source.Keys;

        public IEnumerable<TValue> Values => _source.Values;
    }
}