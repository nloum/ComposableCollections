using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Decorators
{
    public class ReadOnlyWhereDictionaryDecorator<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IComposableReadOnlyDictionary<TKey, TValue> _source;
        private readonly Func<TKey, TValue, bool> _predicate;

        public ReadOnlyWhereDictionaryDecorator(IComposableReadOnlyDictionary<TKey, TValue> source, Func<TKey, TValue, bool> predicate)
        {
            _source = source;
            _predicate = predicate;
        }

        public ReadOnlyWhereDictionaryDecorator(IComposableReadOnlyDictionary<TKey, TValue> source, Func<TValue, bool> predicate) : this(source, (key, value) => predicate(value))
        {
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var maybeValue = _source.TryGetValue(key);
            if ( maybeValue == null )
            {
                value = default;
                return false;
            }
            else
            {
                if (_predicate(key, maybeValue!))
                {
                    value = maybeValue!;
                    return true;
                }
                else
                {
                    value = default;
                    return false;
                }
            }
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