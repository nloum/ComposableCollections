using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMonads;

namespace MoreCollections
{
    public class SystemDelegateDictionaryEx<TKey, TValue> : DictionaryBaseEx<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _wrapped;

        public SystemDelegateDictionaryEx(IDictionary<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        public override bool TryAdd(TKey key, Func<TValue> value)
        {
            if (_wrapped.ContainsKey(key))
            {
                return false;
            }
            
            _wrapped.Add(key, value());
            return true;
        }

        public override bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue)
        {
            if (!_wrapped.TryGetValue(key, out previousValue))
            {
                return false;
            }

            _wrapped[key] = value(previousValue);
            return true;
        }

        public override IMaybe<TValue> AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating, out TValue previousValue)
        {
            if (_wrapped.TryGetValue(key, out previousValue))
            {
                _wrapped[key] = valueIfUpdating(previousValue);
                return previousValue.ToMaybe();
            }
            else
            {
                _wrapped[key] = valueIfAdding();
                return Maybe<TValue>.Nothing();
            }
        }

        public override bool TryRemove(TKey key, out TValue removedItem)
        {
            if (_wrapped.TryGetValue(key, out removedItem))
            {
                _wrapped.Remove(key);
                return true;
            }

            return false;
        }

        public override void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            var result = new DictionaryEx<TKey, TValue>();
            removedItems = result;
            
            foreach (var key in keysToRemove)
            {
                if (!TryGetValue(key, out var previousValue))
                {
                    result.Clear();
                    throw new KeyNotFoundException($"Key not found: {key}");
                }

                result[key] = previousValue;
            }

            _wrapped.RemoveRange(result.Keys);
        }

        public override IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _wrapped.Select(kvp => Utility.KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => _wrapped.Count;

        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;

        public override IEnumerable<TKey> Keys => _wrapped.Keys;

        public override IEnumerable<TValue> Values => _wrapped.Values;

        public override bool ContainsKey(TKey key)
        {
            return _wrapped.ContainsKey(key);
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return _wrapped.TryGetValue(key, out value);
        }

        public override void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _wrapped.AddRange(newItems, key, value);
        }

        public override void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _wrapped.RemoveRange(keysToRemove);
        }

        public override void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            _wrapped.RemoveWhere(predicate);
        }

        public override void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate)
        {
            _wrapped.RemoveWhere((key, value) => predicate(Utility.KeyValuePair(key, value)));
        }

        public override void Clear()
        {
            _wrapped.Clear();
        }

        public override bool TryRemove(TKey key)
        {
            if (_wrapped.ContainsKey(key))
            {
                _wrapped.Remove(key);
                return true;
            }

            return false;
        }
    }
}