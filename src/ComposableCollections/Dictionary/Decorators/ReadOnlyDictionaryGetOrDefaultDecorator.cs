using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Decorators
{
    public class ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        private IComposableReadOnlyDictionary<TKey, TValue> _source;
        private GetDefaultValue<TKey, TValue> _getDefaultValue;

        public ReadOnlyDictionaryGetOrDefaultDecorator(IComposableReadOnlyDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            _source = source;
            _getDefaultValue = getDefaultValue;
        }
        
        public ReadOnlyDictionaryGetOrDefaultDecorator(IComposableReadOnlyDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue)
        {
            _source = source;
            _getDefaultValue = (TKey key, out TValue value) =>
            {
                value = getDefaultValue(key);
                return true;
            };
        }

        protected ReadOnlyDictionaryGetOrDefaultDecorator()
        {
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            _source = source;
            _getDefaultValue = getDefaultValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var maybeValue = _source.TryGetValue(key);
            if (!maybeValue.HasValue)
            {
                return _getDefaultValue(key, out value);
            }
            else
            {
                value = maybeValue.Value;
            }

            return true;
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