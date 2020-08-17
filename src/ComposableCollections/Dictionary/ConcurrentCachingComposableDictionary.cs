using System.Collections.Generic;
using System.Collections.Immutable;

namespace ComposableCollections.Dictionary
{
    public class ConcurrentCachingComposableDictionary<TKey, TValue> : ComposableDictionaryBase<TKey, TValue>, ICachingComposableDictionary<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TValue> _wrapped;
        private readonly IComposableDictionary<TKey, TValue> _flushCacheTo;
        protected readonly object _lock = new object();
        private ImmutableList<DictionaryMutation<TKey, TValue>> _mutations = ImmutableList<DictionaryMutation<TKey, TValue>>.Empty;

        public ConcurrentCachingComposableDictionary(IComposableDictionary<TKey, TValue> flushCacheTo, IComposableDictionary<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
            _flushCacheTo = flushCacheTo;
            _wrapped.AddRange(_flushCacheTo);
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

        public IReadOnlyDictionaryEx<TKey, TValue> AsBypassCache()
        {
            return _flushCacheTo;
        }

        public IComposableDictionary<TKey, TValue> AsNeverFlush()
        {
            return _wrapped;
        }

        public virtual void FlushCache()
        {
            _flushCacheTo.Mutate(GetMutations(true), out var _);
        }
        
        public override bool TryGetValue(TKey key, out TValue value)
        {
            return _wrapped.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
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