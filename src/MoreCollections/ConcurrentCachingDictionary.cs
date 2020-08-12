using System.Collections.Generic;
using System.Collections.Immutable;

namespace MoreCollections
{
    public class ConcurrentCachingDictionary<TKey, TValue> : DictionaryBaseEx<TKey, TValue>, ICachingDictionary<TKey, TValue>
    {
        private readonly IDictionaryEx<TKey, TValue> _wrapped;
        private readonly IDictionaryEx<TKey, TValue> _flushCacheTo;
        private readonly object _lock = new object();
        private ImmutableList<DictionaryMutation<TKey, TValue>> _mutations = ImmutableList<DictionaryMutation<TKey, TValue>>.Empty;

        public ConcurrentCachingDictionary(IDictionaryEx<TKey, TValue> wrapped, IDictionaryEx<TKey, TValue> flushCacheTo)
        {
            _wrapped = wrapped;
            _flushCacheTo = flushCacheTo;
            _wrapped.AddRange(_flushCacheTo);
        }

        public IReadOnlyDictionaryEx<TKey, TValue> AsBypassCache()
        {
            return _flushCacheTo;
        }

        public IDictionaryEx<TKey, TValue> AsNeverFlush()
        {
            return _wrapped;
        }

        public void FlushCache()
        {
            var mutationsToFlush = ImmutableList<DictionaryMutation<TKey, TValue>>.Empty;
            lock (_lock)
            {
                mutationsToFlush = _mutations;
                _mutations = ImmutableList<DictionaryMutation<TKey, TValue>>.Empty;
            }

            _flushCacheTo.Mutate(mutationsToFlush, out var _);
        }
        
        public override bool TryGetValue(TKey key, out TValue value)
        {
            return _wrapped.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _wrapped.GetEnumerator();
        }

        public override int Count => _wrapped.Count;
        public override IEqualityComparer<TKey> Comparer => _wrapped.Comparer;
        public override IEnumerable<TKey> Keys => _wrapped.Keys;
        public override IEnumerable<TValue> Values => _wrapped.Values;
        public override void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations, out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results)
        {
            var mutationsList = mutations.ToImmutableList();
            lock (_lock)
            {
                _mutations = _mutations.AddRange(mutationsList);
            }
            _wrapped.Mutate(mutationsList, out results);
        }
    }
}