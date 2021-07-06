using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Exceptions;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Sources
{
    public class ConcurrentWeakDictionary<TKey, TValue> : DictionaryBase<TKey, TValue> where TValue : class
    {
        protected readonly object Lock = new object();
        protected ImmutableDictionary<TKey, WeakReference<TValue>> State = ImmutableDictionary<TKey, WeakReference<TValue>>.Empty;

        public override bool TryGetValue(TKey key, out TValue value)
        {
            lock (Lock)
            {
                if (State.TryGetValue(key, out var weakReference))
                {
                    if (weakReference.TryGetTarget(out value))
                    {
                        return true;
                    }
                }

                value = default;
                return false;
            }
        }

        private IEnumerable<IKeyValue<TKey, TValue>> AsEnumerable()
        {
            foreach (var item in State)
            {
                if (item.Value.TryGetTarget(out var value))
                {
                    yield return new KeyValue<TKey, TValue>(item.Key, value);
                }
            }
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return AsEnumerable().GetEnumerator();
        }

        public override int Count => AsEnumerable().Count();
        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;
        public override IEnumerable<TKey> Keys => AsEnumerable().Select(x => x.Key);
        public override IEnumerable<TValue> Values => AsEnumerable().Select(x => x.Value);
        public override void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            lock (Lock)
            {
                var finalResults = new List<DictionaryWriteResult<TKey, TValue>>();
                results = finalResults;
                foreach (var write in writes)
                {
                    switch (write.Type)
                    {
                        case DictionaryWriteType.Add:
                        {
                            if (State.TryGetValue(write.Key, out var preExistingValueRef))
                            {
                                if (preExistingValueRef.TryGetTarget(out var preExistingValue))
                                {
                                    throw new AddFailedBecauseKeyAlreadyExistsException(write.Key);
                                }
                                else
                                {
                                    State = State.Remove(write.Key);
                                }
                            }
                            
                            var value = write.ValueIfAdding!();
                            State = State.Add(write.Key, new WeakReference<TValue>(value));
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, true, default(TValue), value));
                            break;
                        }
                        case DictionaryWriteType.TryAdd:
                        {
                            if (State.TryGetValue(write.Key, out var preExistingValueRef))
                            {
                                if (preExistingValueRef.TryGetTarget(out var preExistingValue))
                                {
                                    finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, false, preExistingValue, default(TValue)));
                                }
                                else
                                {
                                    State = State.Remove(write.Key);
                                }
                            }
                            
                            var value = write.ValueIfAdding!();
                            State = State.Add(write.Key, new WeakReference<TValue>(value));
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, true, default(TValue), value));
                            break;
                        }
                        case DictionaryWriteType.Update:
                        {
                            if (State.TryGetValue(write.Key, out var preExistingValue))
                            {
                                if (preExistingValue.TryGetTarget(out var previousValue))
                                {
                                    var newValue = write.ValueIfUpdating!(previousValue);
                                    State = State.SetItem(write.Key, new WeakReference<TValue>(newValue));
                                    finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true, previousValue, newValue));
                                }
                                else
                                {
                                    throw new KeyNotFoundException();
                                }
                            }
                            break;
                        }
                        case DictionaryWriteType.TryUpdate:
                        {
                            if (State.TryGetValue(write.Key, out var preExistingValue))
                            {
                                if (preExistingValue.TryGetTarget(out var previousValue))
                                {
                                    var newValue = write.ValueIfUpdating!(previousValue);
                                    State = State.SetItem(write.Key, new WeakReference<TValue>(newValue));
                                    finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true, previousValue, newValue));
                                }
                                else
                                {
                                    finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, false, default(TValue), default(TValue)));
                                }
                            }
                            break;
                        }
                        case DictionaryWriteType.AddOrUpdate:
                        {
                            if (State.TryGetValue(write.Key, out var preExistingValue))
                            {
                                if (preExistingValue.TryGetTarget(out var previousValue))
                                {
                                    var newValue = write.ValueIfUpdating!(previousValue);
                                    State = State.SetItem(write.Key, new WeakReference<TValue>(newValue));
                                    finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAddOrUpdate(write.Key, DictionaryItemAddOrUpdateResult.Update, previousValue, newValue));
                                    break;
                                }
                                else
                                {
                                    State = State.Remove(write.Key);
                                }
                            }

                            var value = write.ValueIfAdding!();
                            State = State.SetItem(write.Key, new WeakReference<TValue>(value));
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAddOrUpdate(write.Key, DictionaryItemAddOrUpdateResult.Add, default(TValue), value));
                            
                            break;
                        }
                        case DictionaryWriteType.Remove:
                        {
                            if (State.TryGetValue(write.Key, out var previousValueRef))
                            {
                                if (previousValueRef.TryGetTarget(out var previousValue))
                                {
                                    State = State.Remove(write.Key);
                                    finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, previousValue));
                                    break;
                                }
                                else
                                {
                                    State = State.Remove(write.Key);
                                }
                            }

                            throw new KeyNotFoundException();
                            break;
                        }
                        case DictionaryWriteType.TryRemove:
                        {
                            if (State.TryGetValue(write.Key, out var previousValueRef))
                            {
                                if (previousValueRef.TryGetTarget(out var previousValue))
                                {
                                    State.Remove(write.Key);
                                    finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, previousValue));
                                    break;
                                }
                                else
                                {
                                    State = State.Remove(write.Key);
                                }
                            }

                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, default(TValue)));
                            break;
                        }
                        default:
                            throw new ArgumentException($"Unknown mutation type: {write.Type}");
                    }
                }
            }
        }
    }
}