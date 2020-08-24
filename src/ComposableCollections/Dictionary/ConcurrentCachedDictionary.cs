using System.Collections.Generic;
using System.Collections.Immutable;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary
{
    public class ConcurrentCachedDictionary<TKey, TValue> : DictionaryBase<TKey, TValue>, ICachedDictionary<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TValue> _cache;
        private readonly IComposableDictionary<TKey, TValue> _flushCacheTo;
        protected readonly object _lock = new object();
        private ImmutableList<DictionaryWrite<TKey, TValue>> _writes = ImmutableList<DictionaryWrite<TKey, TValue>>.Empty;

        public ConcurrentCachedDictionary(IComposableDictionary<TKey, TValue> flushCacheTo, IComposableDictionary<TKey, TValue> cache)
        {
            _cache = cache;
            _flushCacheTo = flushCacheTo;
            _cache.AddRange(_flushCacheTo);
        }

        public IEnumerable<DictionaryWrite<TKey, TValue>> GetWrites(bool clear)
        {
            if (!clear)
            {
                return _writes;
            }
            else
            {
                lock (_lock)
                {
                    return GetWritesAndClear();
                }
            }
        }

        protected IEnumerable<DictionaryWrite<TKey, TValue>> GetWritesAndClear()
        {
            var writesToFlush = _writes;
            _writes = ImmutableList<DictionaryWrite<TKey, TValue>>.Empty;
            return writesToFlush;
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
            _flushCacheTo.Write(GetWrites(true), out var _);
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
        public override void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            var writesList = writes.ToImmutableList();
            lock (_lock)
            {
                _writes = _writes.AddRange(writesList);
            }
            _cache.Write(writesList, out results);
        }
    }
}