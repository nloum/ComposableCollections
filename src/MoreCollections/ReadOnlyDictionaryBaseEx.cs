using System.Collections;
using System.Collections.Generic;
using SimpleMonads;

namespace MoreCollections
{
    public abstract class ReadOnlyDictionaryBaseEx<TKey, TValue> : IReadOnlyDictionaryEx<TKey, TValue>
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public IMaybe<TValue> TryGetValue(TKey key)
        {
            if (TryGetValue(key, out var value))
            {
                return value.ToMaybe();
            }
            
            return Maybe<TValue>.Nothing();
        }

        public abstract bool TryGetValue(TKey key, out TValue value);
        
        public TValue this[TKey key]
        {
            get
            {
                if (!this.TryGetValue(key, out var value))
                {
                    throw new KeyNotFoundException($"Key not found: {key}");
                }

                return value;
            }
        }
        
        public virtual bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var value);
        }
        
        public abstract IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator();

        public abstract int Count { get; }

        public abstract IEqualityComparer<TKey> Comparer { get; }

        public abstract IEnumerable<TKey> Keys { get; }

        public abstract IEnumerable<TValue> Values { get; }
    }
}