using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Base
{
    public abstract class ReadOnlyDictionaryBase<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        #region Abstract members

        public abstract bool TryGetValue(TKey key, out TValue value);

        public abstract IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator();

        public abstract int Count { get; }

        public abstract IEqualityComparer<TKey> Comparer { get; }

        public abstract IEnumerable<TKey> Keys { get; }

        public abstract IEnumerable<TValue> Values { get; }
        
        #endregion
        
        public TValue GetValue(TKey key)
        {
            return this[key];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public TValue? TryGetValue(TKey key)
        {
            if (TryGetValue(key, out var value))
            {
                return value;
            }
            
            return default(TValue);
        }

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
    }
}