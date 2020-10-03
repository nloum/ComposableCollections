using System.Collections.Generic;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;

namespace ComposableCollections.Dictionary.Adapters
{
    public class ReadCachedReadOnlyDictionaryAdapter<TKey, TValue> : ReadOnlyDictionaryBase<TKey, TValue>, IReadCachedReadOnlyDictionary<TKey, TValue>
    {
        private IComposableReadOnlyDictionary<TKey, TValue> _innerValues;
        private IComposableDictionary<TKey, TValue> _cache = new ComposableDictionary<TKey, TValue>();
        private bool _isEntireCacheInvalidated = true;
        private ISet<TKey> _invalidatedKeys = new HashSet<TKey>();
        private readonly object _lock = new object();

        public ReadCachedReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TValue> innerValues)
        {
            _innerValues = innerValues;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            lock (_lock)
            {
                if (_isEntireCacheInvalidated || _invalidatedKeys.Contains(key))
                {
                    ReloadCacheInternal(key);
                }

                return _cache.TryGetValue(key, out value);
            }
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            lock (_lock)
            {
                if (_isEntireCacheInvalidated || _invalidatedKeys.Count > 0)
                {
                    ReloadCacheInternal();
                }

                return _cache.GetEnumerator();
            }
        }

        public override int Count
        {
            get
            {
                lock (_lock)
                {
                    if (_isEntireCacheInvalidated || _invalidatedKeys.Count > 0)
                    {
                        ReloadCacheInternal();
                    }

                    return _cache.Count;
                }
            }
        }

        public override IEqualityComparer<TKey> Comparer => _innerValues.Comparer;

        public override IEnumerable<TKey> Keys
        {
            get
            {
                lock (_lock)
                {
                    if (_isEntireCacheInvalidated || _invalidatedKeys.Count > 0)
                    {
                        ReloadCacheInternal();
                    }

                    return _cache.Keys;
                }
            }
        }

        public override IEnumerable<TValue> Values
        {
            get
            {
                lock (_lock)
                {
                    if (_isEntireCacheInvalidated || _invalidatedKeys.Count > 0)
                    {
                        ReloadCacheInternal();
                    }

                    return _cache.Values;
                }
            }
        }

        public void ReloadCache()
        {
            lock (_lock)
            {
                ReloadCacheInternal();
            }
        }

        private void ReloadCacheInternal()
        {
            _cache.Clear();
            _cache.AddRange(_innerValues);
            
            _isEntireCacheInvalidated = false;
            _invalidatedKeys.Clear();
        }

        public void ReloadCache(TKey key)
        {
            lock (_lock)
            {
                ReloadCacheInternal(key);
            }
        }

        private void ReloadCacheInternal(TKey key)
        {
            var value = _innerValues.TryGetValue(key);
            if (value.HasValue)
            {
                _cache[key] = value.Value;
            }
            else
            {
                _cache.TryRemove(key);
            }
            
            _invalidatedKeys.Remove(key);
        }

        public void InvalidCache()
        {
            lock (_lock)
            {
                _isEntireCacheInvalidated = true;
            }
        }

        public void InvalidCache(TKey key)
        {
            lock (_lock)
            {
                _invalidatedKeys.Add(key);
            }
        }
    }
}