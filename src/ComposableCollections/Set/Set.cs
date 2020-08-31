using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections.Set.Exceptions;
using ComposableCollections.Set.Write;
using SimpleMonads;

namespace ComposableCollections.Set
{
    public class ConcurrentSet<TValue> : ISet<TValue>
    {
        private ImmutableHashSet<TValue> _state = ImmutableHashSet<TValue>.Empty;
        private readonly object _lock = new object();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _state.GetEnumerator();
        }

        public void Write(IEnumerable<SetWrite<TValue>> writes, out IReadOnlyList<SetWriteResult<TValue>> results)
        {
            lock (_lock)
            {
                var finalResults = new List<SetWriteResult<TValue>>();
                results = finalResults;
                var tmpState = _state;
                foreach(var write in writes)
                {
                    if (write.Type == SetWriteType.Add)
                    {
                        if (!tmpState.Contains(write.Value))
                        {
                            tmpState = tmpState.Add(write.Value);
                        }
                        else 
                        {
                            throw new AddFailedBecauseValueAlreadyExistsException(write.Value);
                        }
                    }
                    else if (write.Type == SetWriteType.TryAdd)
                    {
                        if (!tmpState.Contains(write.Value))
                        {
                            tmpState = tmpState.Add(write.Value);
                        }
                    }
                    else if (write.Type == SetWriteType.Remove)
                    {
                        if (tmpState.Contains(write.Value))
                        {
                            tmpState = tmpState.Remove(write.Value);
                        }
                        else 
                        {
                            throw new RemoveFailedBecauseNoSuchValueExistsException(write.Value);
                        }
                    }
                    else if (write.Type == SetWriteType.TryRemove)
                    {
                        if (tmpState.Contains(write.Value))
                        {
                            tmpState = tmpState.Remove(write.Value);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unknown write type {write.Type}");
                    }
                }

                _state = tmpState;
            }
        }

        public void Add(TValue item)
        {
            lock (_lock)
            {
                _state = _state.Add(item);
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _state = ImmutableHashSet<TValue>.Empty;
            }
        }

        public bool Contains(TValue item)
        {
            return _state.Contains(item);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            foreach (var item in _state)
            {
                array[arrayIndex] = item;
                arrayIndex++;
                if (arrayIndex >= array.Length)
                {
                    break;
                }
            }
        }

        public void Remove(TValue item)
        {
            lock (_lock)
            {
                _state = _state.Remove(item);
            }
        }

        int IReadOnlyCollection<TValue>.Count => _state.Count;

        public bool TryAdd(TValue value)
        {
            lock (_lock)
            {
                if (_state.Contains(value))
                {
                    return false;
                }

                _state = _state.Add(value);
                return true;
            }
        }
        
        public void TryAddRange(IEnumerable<TValue> newItems, out IReadOnlySet<ISetItemAddAttempt<TValue>> results)
        {
            lock (_lock)
            {
                var tmpState = _state;
                var finalResults = new ConcurrentSet<ISetItemAddAttempt<TValue>>();
                results = finalResults;
                foreach (var newItem in newItems)
                {
                    if (!tmpState.Contains(newItem))
                    {
                        tmpState = tmpState.Add(newItem);
                        finalResults.Add(new SetItemAddAttempt<TValue>(true, newItem));
                    }
                    else
                    {
                        finalResults.Add(new SetItemAddAttempt<TValue>(false, newItem));
                    }
                }

                _state = tmpState;
            }
        }

        public void TryAddRange(IEnumerable<TValue> newItems)
        {
            lock (_lock)
            {
                var tmpState = _state;
                foreach (var newItem in newItems)
                {
                    if (tmpState.Contains(newItem))
                    {
                        continue;
                    }
                    tmpState = tmpState.Add(newItem);
                }

                _state = tmpState;
            }
        }

        public void TryAddRange(params TValue[] newItems)
        {
            TryAddRange(newItems.AsEnumerable());
        }

        public void AddRange(IEnumerable<TValue> newItems)
        {
            lock (_lock)
            {
                var tmpState = _state;
                foreach (var newItem in newItems)
                {
                    tmpState = tmpState.Add(newItem);
                }

                _state = tmpState;
            }
        }

        public void AddRange(params TValue[] newItems)
        {
            AddRange(newItems.AsEnumerable());
        }

        public void TryRemoveRange(IEnumerable<TValue> valuesToRemove)
        {
            lock (_lock)
            {
                foreach (var valueToRemove in valuesToRemove)
                {
                    if (_state.Contains(valueToRemove))
                    {
                        _state = _state.Remove(valueToRemove);
                    }
                }
            }
        }

        public void RemoveRange(IEnumerable<TValue> valuesToRemove)
        {
            lock (_lock)
            {
                foreach (var valueToRemove in valuesToRemove)
                {
                    if (_state.Contains(valueToRemove))
                    {
                        _state = _state.Remove(valueToRemove);
                    }
                    else
                    {
                        throw new KeyNotFoundException();
                    }
                }
            }
        }

        public void RemoveWhere(Func<TValue, bool> predicate)
        {
            lock (_lock)
            {
                foreach (var item in _state)
                {
                    if (predicate(item))
                    {
                        _state = _state.Remove(item);
                    }
                }
            }
        }

        public bool TryRemove(TValue value)
        {
            lock (_lock)
            {
                if (_state.Contains(value))
                {
                    _state = _state.Remove(value);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void TryRemoveRange(IEnumerable<TValue> valuesToRemove, out IReadOnlySet<TValue> removedItems)
        {
            lock (_lock)
            {                
                var results = new ConcurrentSet<TValue>();
                removedItems = results;
                foreach (var valueToRemove in valuesToRemove)
                {
                    if (_state.Contains(valueToRemove))
                    {
                        _state = _state.Remove(valueToRemove);
                        results.Add(valueToRemove);
                    }
                }
            }
        }

        public void RemoveRange(IEnumerable<TValue> valuesToRemove, out IReadOnlySet<TValue> removedItems)
        {
            lock (_lock)
            {
                var results = new ConcurrentSet<TValue>();
                removedItems = results;
                var tmpState = _state;
                foreach (var valueToRemove in valuesToRemove)
                {
                    if (tmpState.Contains(valueToRemove))
                    {
                        tmpState = tmpState.Remove(valueToRemove);
                        results.Add(valueToRemove);
                    }
                    else
                    {
                        throw new KeyNotFoundException();
                    }
                }

                _state = tmpState;
            }
        }

        public void RemoveWhere(Func<TValue, bool> predicate, out IReadOnlySet<TValue> removedItems)
        {
            lock (_lock)
            {
                var results = new ConcurrentSet<TValue>();
                removedItems = results;
                var tmpState = _state;
                foreach (var value in _state)
                {
                    if (predicate(value))
                    {
                        tmpState = tmpState.Remove(value);
                        results.Add(value);
                    }
                }

                _state = tmpState;
            }
        }

        public void Clear(out IReadOnlySet<TValue> removedItems)
        {
            lock (_lock)
            {
                var results = new ConcurrentSet<TValue>();
                results.AddRange(_state);
                _state = ImmutableHashSet<TValue>.Empty;
                removedItems = results;
            }
        }
    }
}