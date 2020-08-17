using System.Collections.Generic;
using System.Collections.Immutable;

namespace ComposableCollections.Dictionary
{
    public class ConcurrentCachingDictionary<TKey, TValue> : DictionaryBase<TKey, TValue>, ICacheDictionary<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TValue> _cache;
        private readonly IComposableDictionary<TKey, TValue> _flushCacheTo;
        protected readonly object _lock = new object();
        private ImmutableList<DictionaryMutation<TKey, TValue>> _mutations = ImmutableList<DictionaryMutation<TKey, TValue>>.Empty;

        public ConcurrentCachingDictionary(IComposableDictionary<TKey, TValue> flushCacheTo, IComposableDictionary<TKey, TValue> cache)
        {
            _cache = cache;
            _flushCacheTo = flushCacheTo;
            _cache.AddRange(_flushCacheTo);
        }

        public IEnumerable<DictionaryMutation<TKey, TValue>> GetMutations(bool clear)
        {
            if (!clear)
            {
                return _mutations;
            }
            else
            {
                lock (_lock)
                {
                    return GetMutationsAndClear();
                }
            }
        }

        protected IEnumerable<DictionaryMutation<TKey, TValue>> GetMutationsAndClear()
        {
            var mutationsToFlush = _mutations;
            _mutations = ImmutableList<DictionaryMutation<TKey, TValue>>.Empty;
            return mutationsToFlush;
        }

        public IComposableReadOnlyDictionary<TKey, TValue> AsBypassCache()
        {
            return _flushCacheTo;
        }

        public IComposableDictionary<TKey, TValue> AsNeverFlush()
        {
            return _cache;
        }

        public virtual void FlushCache()
        {
            _flushCacheTo.Mutate(GetMutations(true), out var _);
        }
        
        public override bool TryGetValue(TKey key, out TValue value)
        {
            return _cache.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _cache.GetEnumerator();
        }

        public override int Count => _cache.Count;
        public override IEqualityComparer<TKey> Comparer => _cache.Comparer;
        public override IEnumerable<TKey> Keys => _cache.Keys;
        public override IEnumerable<TValue> Values => _cache.Values;
        public override void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations, out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results)
        {
            var mutationsList = mutations.ToImmutableList();
            lock (_lock)
            {
                _mutations = _mutations.AddRange(mutationsList);
            }
            _cache.Mutate(mutationsList, out results);
        }
    }
}