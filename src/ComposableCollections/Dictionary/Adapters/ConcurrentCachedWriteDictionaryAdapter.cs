using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Exceptions;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Adapters
{
    public class ConcurrentCachedWriteDictionaryAdapter<TKey, TValue> : DictionaryBase<TKey, TValue>, ICachedDictionary<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TValue> _addedOrUpdated;
        private readonly IComposableDictionary<TKey, TValue> _removed;
        private readonly IComposableDictionary<TKey, TValue> _flushCacheTo;
        protected readonly object _lock = new object();
        private ImmutableList<DictionaryWrite<TKey, TValue>> _writes = ImmutableList<DictionaryWrite<TKey, TValue>>.Empty;

        public ConcurrentCachedWriteDictionaryAdapter(IComposableDictionary<TKey, TValue> flushCacheTo, IComposableDictionary<TKey, TValue> addedOrUpdated = null, IComposableDictionary<TKey, TValue> removed = null)
        {
            _addedOrUpdated = addedOrUpdated ?? new ComposableDictionary<TKey, TValue>();
            _removed = removed ?? new ComposableDictionary<TKey, TValue>();
            _flushCacheTo = flushCacheTo;
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
            throw new NotImplementedException();
        }

        public virtual void FlushCache()
        {
            _flushCacheTo.Write(GetWrites(true), out var _);
        }
        
        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (_addedOrUpdated.TryGetValue(key, out value))
            {
                return true;
            }

            if (_removed.ContainsKey(key))
            {
                return false;
            }
            
            return _flushCacheTo.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _addedOrUpdated.Concat(_flushCacheTo.Where(x => !_removed.ContainsKey(x.Key))).GetEnumerator();
        }

        public override int Count => _addedOrUpdated.Count - _removed.Count + _flushCacheTo.Count;
        public override IEqualityComparer<TKey> Comparer => _flushCacheTo.Comparer;
        public override IEnumerable<TKey> Keys => _addedOrUpdated.Keys.Concat(_flushCacheTo.Keys.Where(key => !_removed.ContainsKey(key)));
        public override IEnumerable<TValue> Values => this.Select(x => x.Value);
        public override void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            var writesList = writes.ToImmutableList();
            lock (_lock)
            {
                var finalResults = new List<DictionaryWriteResult<TKey, TValue>>();
                results = finalResults;

                _writes = _writes.AddRange(writesList);
                foreach (var write in writesList)
                {
                    if (write.Type == DictionaryWriteType.Remove)
                    {
                        if (_addedOrUpdated.TryRemove(write.Key, out var removedValue))
                        {
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, removedValue.ToMaybe()));
                        }
                        else
                        {
                            removedValue = _flushCacheTo[write.Key];
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, removedValue.ToMaybe()));
                            _removed.Add(write.Key, removedValue);
                        }
                    }
                    else if (write.Type == DictionaryWriteType.TryRemove)
                    {
                        if (_addedOrUpdated.TryRemove(write.Key, out var removedValue))
                        {
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, removedValue.ToMaybe()));
                        }
                        else
                        {
                            if (_flushCacheTo.TryGetValue(write.Key, out removedValue))
                            {
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, removedValue.ToMaybe()));
                                _removed.Add(write.Key, removedValue);
                            }
                            else
                            {
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, Maybe<TValue>.Nothing()));
                            }
                        }
                    }
                    else if (write.Type == DictionaryWriteType.Add)
                    {
                        if (_addedOrUpdated.ContainsKey(write.Key) || (_flushCacheTo.ContainsKey(write.Key) &&
                                                                          !_removed.ContainsKey(write.Key)))
                        {
                            throw new AddFailedBecauseKeyAlreadyExistsException(write.Key);
                        }
                        
                        _addedOrUpdated.Write(new[]{write}, out var tmpResults);
                        finalResults.AddRange(tmpResults);
                    }
                    else if (write.Type == DictionaryWriteType.TryAdd)
                    {
                        if (_addedOrUpdated.TryGetValue(write.Key, out var preExistingValue))
                        {
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateTryAdd(write.Key, false, preExistingValue.ToMaybe(), Maybe<TValue>.Nothing()));
                        }
                        else if (!_removed.ContainsKey(write.Key) && _flushCacheTo.TryGetValue(write.Key, out preExistingValue))
                        {
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateTryAdd(write.Key, false, preExistingValue.ToMaybe(), Maybe<TValue>.Nothing()));
                        }
                        else
                        {
                            _addedOrUpdated.Write(new[]{write}, out var tmpResults);
                            finalResults.AddRange(tmpResults);
                        }
                    }
                    else if (write.Type == DictionaryWriteType.Update)
                    {
                        if (_addedOrUpdated.TryGetValue(write.Key, out var preExistingValue))
                        {
                            _addedOrUpdated.Write(new[]{write}, out var tmpResults);
                            finalResults.AddRange(tmpResults);
                        }
                        else if (!_removed.ContainsKey(write.Key) && _flushCacheTo.TryGetValue(write.Key, out preExistingValue))
                        {
                            var newValue = write.ValueIfUpdating.Value(preExistingValue);
                            _addedOrUpdated.Add(write.Key, newValue);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true, preExistingValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            throw new KeyNotFoundException();
                        }
                    }
                    else if (write.Type == DictionaryWriteType.TryUpdate)
                    {
                        if (_addedOrUpdated.TryGetValue(write.Key, out var preExistingValue))
                        {
                            _addedOrUpdated.Write(new[]{write}, out var tmpResults);
                            finalResults.AddRange(tmpResults);
                        }
                        else if (!_removed.ContainsKey(write.Key) && _flushCacheTo.TryGetValue(write.Key, out preExistingValue))
                        {
                            var newValue = write.ValueIfUpdating.Value(preExistingValue);
                            _addedOrUpdated.Add(write.Key, newValue);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true, preExistingValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, false, Maybe<TValue>.Nothing(), Maybe<TValue>.Nothing()));
                        }
                    }
                    else if (write.Type == DictionaryWriteType.AddOrUpdate)
                    {
                        if (_addedOrUpdated.TryGetValue(write.Key, out var preExistingValue))
                        {
                            _addedOrUpdated.Write(new[]{write}, out var tmpResults);
                            finalResults.AddRange(tmpResults);
                        }
                        else if (!_removed.ContainsKey(write.Key) && _flushCacheTo.TryGetValue(write.Key, out preExistingValue))
                        {
                            var newValue = write.ValueIfUpdating.Value(preExistingValue);
                            _addedOrUpdated.Add(write.Key, newValue);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true, preExistingValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            _addedOrUpdated.Write(new[]{write}, out var tmpResults);
                            finalResults.AddRange(tmpResults);
                        }
                    }
                }
            }
        }
    }
}