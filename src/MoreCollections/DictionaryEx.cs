using System.Collections.Generic;
using System.Linq;

namespace MoreCollections
{
    public class DictionaryEx<TKey, TValue> : DictionaryBaseEx<TKey, TValue>
    {
        protected readonly Dictionary<TKey, TValue> State = new Dictionary<TKey, TValue>();

        public override bool TryAdd(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                return false;
            }
            
            State.Add(key, value);
            return true;
        }

        public override bool TryUpdate(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                State[key] = value;
                return true;
            }
            
            return false;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return State.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return State.Select(kvp => Utility.KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => State.Count;

        public override IEqualityComparer<TKey> Comparer => State.Comparer;
        public override IEnumerable<TKey> Keys => State.Keys;
        public override IEnumerable<TValue> Values => State.Values;
        public override AddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                State[key] = value;
                return AddOrUpdateResult.Update;
            }
            
            Add(key, value);
            return AddOrUpdateResult.Add;
        }

        public override bool TryRemove(TKey key)
        {
            if (ContainsKey(key))
            {
                State.Remove(key);
                return true;
            }

            return false;
        }
    }
}