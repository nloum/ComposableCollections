using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections.Dictionary.Exceptions;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public class ConcurrentCachingDictionaryWithMinimalState<TKey, TValue> : DictionaryBase<TKey, TValue>, ICacheDictionary<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TValue> _addedOrUpdated;
        private readonly IComposableDictionary<TKey, TValue> _removed;
        private readonly IComposableDictionary<TKey, TValue> _flushCacheTo;
        protected readonly object _lock = new object();
        private ImmutableList<DictionaryMutation<TKey, TValue>> _mutations = ImmutableList<DictionaryMutation<TKey, TValue>>.Empty;

        public ConcurrentCachingDictionaryWithMinimalState(IComposableDictionary<TKey, TValue> flushCacheTo, IComposableDictionary<TKey, TValue> addedOrUpdated = null, IComposableDictionary<TKey, TValue> removed = null)
        {
            _addedOrUpdated = addedOrUpdated ?? new ComposableDictionary<TKey, TValue>();
            _removed = removed ?? new ComposableDictionary<TKey, TValue>();
            _flushCacheTo = flushCacheTo;
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
            throw new NotImplementedException();
        }

        public virtual void FlushCache()
        {
            _flushCacheTo.Mutate(GetMutations(true), out var _);
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
        public override void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations, out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results)
        {
            var mutationsList = mutations.ToImmutableList();
            lock (_lock)
            {
                var finalResults = new List<DictionaryMutationResult<TKey, TValue>>();
                results = finalResults;

                _mutations = _mutations.AddRange(mutationsList);
                foreach (var mutation in mutationsList)
                {
                    if (mutation.Type == DictionaryMutationType.Remove)
                    {
                        if (_addedOrUpdated.TryRemove(mutation.Key, out var removedValue))
                        {
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key, removedValue.ToMaybe()));
                        }
                        else
                        {
                            removedValue = _flushCacheTo[mutation.Key];
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key, removedValue.ToMaybe()));
                            _removed.Add(mutation.Key, removedValue);
                        }
                    }
                    else if (mutation.Type == DictionaryMutationType.TryRemove)
                    {
                        if (_addedOrUpdated.TryRemove(mutation.Key, out var removedValue))
                        {
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key, removedValue.ToMaybe()));
                        }
                        else
                        {
                            if (_flushCacheTo.TryGetValue(mutation.Key, out removedValue))
                            {
                                finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key, removedValue.ToMaybe()));
                                _removed.Add(mutation.Key, removedValue);
                            }
                            else
                            {
                                finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key, Maybe<TValue>.Nothing()));
                            }
                        }
                    }
                    else if (mutation.Type == DictionaryMutationType.Add)
                    {
                        if (_addedOrUpdated.ContainsKey(mutation.Key) || (_flushCacheTo.ContainsKey(mutation.Key) &&
                                                                          !_removed.ContainsKey(mutation.Key)))
                        {
                            throw new AddFailedBecauseKeyAlreadyExistsException(mutation.Key);
                        }
                        
                        _addedOrUpdated.Mutate(new[]{mutation}, out var tmpResults);
                        finalResults.AddRange(tmpResults);
                    }
                    else if (mutation.Type == DictionaryMutationType.TryAdd)
                    {
                        if (_addedOrUpdated.TryGetValue(mutation.Key, out var preExistingValue))
                        {
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateTryAdd(mutation.Key, false, preExistingValue.ToMaybe(), Maybe<TValue>.Nothing()));
                        }
                        else if (!_removed.ContainsKey(mutation.Key) && _flushCacheTo.TryGetValue(mutation.Key, out preExistingValue))
                        {
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateTryAdd(mutation.Key, false, preExistingValue.ToMaybe(), Maybe<TValue>.Nothing()));
                        }
                        else
                        {
                            _addedOrUpdated.Mutate(new[]{mutation}, out var tmpResults);
                            finalResults.AddRange(tmpResults);
                        }
                    }
                    else if (mutation.Type == DictionaryMutationType.Update)
                    {
                        if (_addedOrUpdated.TryGetValue(mutation.Key, out var preExistingValue))
                        {
                            _addedOrUpdated.Mutate(new[]{mutation}, out var tmpResults);
                            finalResults.AddRange(tmpResults);
                        }
                        else if (!_removed.ContainsKey(mutation.Key) && _flushCacheTo.TryGetValue(mutation.Key, out preExistingValue))
                        {
                            var newValue = mutation.ValueIfUpdating.Value(preExistingValue);
                            _addedOrUpdated.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true, preExistingValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            throw new KeyNotFoundException();
                        }
                    }
                    else if (mutation.Type == DictionaryMutationType.TryUpdate)
                    {
                        if (_addedOrUpdated.TryGetValue(mutation.Key, out var preExistingValue))
                        {
                            _addedOrUpdated.Mutate(new[]{mutation}, out var tmpResults);
                            finalResults.AddRange(tmpResults);
                        }
                        else if (!_removed.ContainsKey(mutation.Key) && _flushCacheTo.TryGetValue(mutation.Key, out preExistingValue))
                        {
                            var newValue = mutation.ValueIfUpdating.Value(preExistingValue);
                            _addedOrUpdated.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true, preExistingValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, false, Maybe<TValue>.Nothing(), Maybe<TValue>.Nothing()));
                        }
                    }
                    else if (mutation.Type == DictionaryMutationType.AddOrUpdate)
                    {
                        if (_addedOrUpdated.TryGetValue(mutation.Key, out var preExistingValue))
                        {
                            _addedOrUpdated.Mutate(new[]{mutation}, out var tmpResults);
                            finalResults.AddRange(tmpResults);
                        }
                        else if (!_removed.ContainsKey(mutation.Key) && _flushCacheTo.TryGetValue(mutation.Key, out preExistingValue))
                        {
                            var newValue = mutation.ValueIfUpdating.Value(preExistingValue);
                            _addedOrUpdated.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true, preExistingValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            _addedOrUpdated.Mutate(new[]{mutation}, out var tmpResults);
                            finalResults.AddRange(tmpResults);
                        }
                    }
                }
            }
        }
    }
}