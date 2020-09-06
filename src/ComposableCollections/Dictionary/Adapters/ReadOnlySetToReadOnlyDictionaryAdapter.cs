using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Adapters
{
    public class ReadOnlySetToReadOnlyDictionaryAdapter<TKey> : IComposableReadOnlyDictionary<TKey, TKey>
    {
        private readonly Set.IReadOnlySet<TKey> _set;
        
        public ReadOnlySetToReadOnlyDictionaryAdapter(Set.IReadOnlySet<TKey> set)
        {
            _set = set;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TKey>> GetEnumerator()
        {
            return _set.Select(key => new KeyValue<TKey, TKey>(key, key)).GetEnumerator();
        }

        public int Count => _set.Count;
        public IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;

        public TKey GetValue(TKey key)
        {
            return this[key];
        }

        public TKey this[TKey key] => TryGetValue(key).Value;

        public IEnumerable<TKey> Keys => _set;
        public IEnumerable<TKey> Values => _set;
        public bool ContainsKey(TKey key)
        {
            return _set.Contains(key);
        }

        public IMaybe<TKey> TryGetValue(TKey key)
        {
            if (ContainsKey(key))
            {
                return key.ToMaybe();
            }
            
            return Maybe<TKey>.Nothing();
        }
    }
}