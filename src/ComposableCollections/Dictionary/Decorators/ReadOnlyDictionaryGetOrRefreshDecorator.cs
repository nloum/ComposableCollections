using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Decorators
{
    public class ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IComposableReadOnlyDictionary<TKey, TValue> _source;
        private readonly RefreshValue<TKey, TValue> _refreshValue;

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var maybeValue = _source.TryGetValue(key);
            if ( maybeValue != null )
            {
                if (_refreshValue(key, maybeValue!, out value))
                {
                    return true;
                }
                else
                {
                    value = maybeValue!;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public TValue? TryGetValue(TKey key)
        {
            if (TryGetValue(key, out var value))
            {
                return value;
            }
            
            return default(TValue);
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