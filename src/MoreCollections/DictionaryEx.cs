using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMonads;

namespace MoreCollections
{
    public class DictionaryEx<TKey, TValue> : DictionaryBaseEx<TKey, TValue>
    {
        protected readonly Dictionary<TKey, TValue> State = new Dictionary<TKey, TValue>();

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return State.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return State.Select(kvp => Utility.KeyValuePair(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => State.Count;
        public override IEqualityComparer<TKey> Comparer => State.Comparer;
        public override IEnumerable<TKey> Keys => State.Keys;
        public override IEnumerable<TValue> Values => State.Values;
        public override bool TryAdd(TKey key, Func<TValue> value, out TValue newValue)
        {
            if (State.ContainsKey(key))
            {
                newValue = default;
                return false;
            }

            newValue = value();
            State.Add(key, newValue);
            return true;
        }

        public override bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            if (TryGetValue(key, out previousValue))
            {
                newValue = value(previousValue);
                State[key] = newValue;
                return true;
            }

            newValue = default;
            return false;
        }

        public override IMaybe<TValue> AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating, out TValue previousValue, out TValue newValue)
        {
            if (TryGetValue(key, out previousValue))
            {
                newValue = valueIfUpdating(previousValue);
                State[key] = newValue;
                return previousValue.ToMaybe();
            }
            else
            {
                previousValue = default;
                newValue = valueIfAdding();
                State.Add(key, newValue);
                return Maybe<TValue>.Nothing();
            }
        }

        public override bool TryRemove(TKey key, out TValue removedItem)
        {
            if (TryGetValue(key, out removedItem))
            {
                State.Remove(key);
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

            State.RemoveRange(result.Keys);
        }
    }
}