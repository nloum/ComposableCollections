using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollections
{
    public class SystemDelegateDictionaryEx<TKey, TValue> : DictionaryBaseEx<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _wrapped;

        public SystemDelegateDictionaryEx(IDictionary<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        public override bool TryAdd(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                return false;
            }
            
            _wrapped.Add(key, value);
            return true;
        }
        
        public override bool TryUpdate(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                _wrapped[key] = value;
                return true;
            }

            return false;
        }
        
        public override AddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                _wrapped[key] = value;
                return AddOrUpdateResult.Update;
            }
            
            _wrapped.Add(key, value);
            return AddOrUpdateResult.Add;
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