using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections.Set.Base;
using ComposableCollections.Set.Exceptions;
using ComposableCollections.Set.Write;

namespace ComposableCollections.Set.Adapters
{
    public class CachedSetAdapter<TValue> : ComposableSetBase<TValue>, ICachedSet<TValue>
    {
        private readonly IComposableSet<TValue> _flushTo;
        private readonly IComposableSet<TValue> _addedOrUpdated;
        private readonly IComposableSet<TValue> _removed;
        private readonly object _lock = new object();
        private ImmutableList<SetWrite<TValue>> _writes = ImmutableList<SetWrite<TValue>>.Empty;
        
        public override IEnumerator<TValue> GetEnumerator()
        {
            return _flushTo.Except(_removed).Except(_addedOrUpdated).Concat(_addedOrUpdated).GetEnumerator();
        }

        public override int Count => _flushTo.Count - _removed.Count + _addedOrUpdated.Count;
        public override bool Contains(TValue item)
        {
            return _addedOrUpdated.Contains(item) || (_flushTo.Contains(item) && !_removed.Contains(item));
        }

        public override void Write(IEnumerable<SetWrite<TValue>> writes, out IReadOnlyList<SetWriteResult<TValue>> results)
        {
            lock (_lock)
            {
                var finalResults = new List<SetWriteResult<TValue>>();
                results = finalResults;
                
                foreach (var write in writes)
                {
                    if (write.Type == SetWriteType.Add || write.Type == SetWriteType.TryAdd)
                    {
                        if (_flushTo.Contains(write.Value) || _addedOrUpdated.Contains(write.Value))
                        {
                            if (write.Type == SetWriteType.Add)
                            {
                                throw new AddFailedBecauseValueAlreadyExistsException(write.Value);
                            }
                            
                            finalResults.Add(new SetWriteResult<TValue>(write.Type, false, write.Value));
                        }
                        else
                        {
                            _addedOrUpdated.Add(write.Value);
                            finalResults.Add(new SetWriteResult<TValue>(write.Type, true, write.Value));
                        }
                    }
                    else if (write.Type == SetWriteType.Remove || write.Type == SetWriteType.TryRemove)
                    {
                        if (_removed.Contains(write.Value) || (!_flushTo.Contains(write.Value) && !_addedOrUpdated.Contains(write.Value)))
                        {
                            if (write.Type == SetWriteType.Remove)
                            {
                                throw new RemoveFailedBecauseNoSuchValueExistsException(write.Value);
                            }
                            
                            finalResults.Add(new SetWriteResult<TValue>(write.Type, false, write.Value));
                        }
                        else
                        {
                            _removed.Add(write.Value);
                            finalResults.Add(new SetWriteResult<TValue>(write.Type, true, write.Value));
                        }
                    }
                    
                    _writes = _writes.Add(write);
                }
            }
        }

        public IReadOnlySet<TValue> AsBypassCache()
        {
            return _flushTo;
        }

        public IComposableSet<TValue> AsNeverFlush()
        {
            throw new NotImplementedException();
        }

        public void FlushCache()
        {
            lock (_lock)
            {
                var writes = GetWritesWithClear();
                _addedOrUpdated.Clear();
                _removed.Clear();
                _flushTo.Write(writes, out var results);
            }
        }

        public IEnumerable<SetWrite<TValue>> GetWrites(bool clear)
        {
            if (!clear)
            {
                return _writes;
            }
            else
            {
                lock (_lock)
                {
                    return GetWritesWithClear();
                }
            }
        }

        private IEnumerable<SetWrite<TValue>> GetWritesWithClear()
        {
            var result = _writes;
            _writes = ImmutableList<SetWrite<TValue>>.Empty;
            return result;
        }
    }
}