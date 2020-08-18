using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections.Dictionary.Exceptions;
using SimpleMonads;

namespace ComposableCollections.Dictionary
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
        public override void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations, out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results)
        {
            lock (Lock)
            {
                var finalResults = new List<DictionaryMutationResult<TKey, TValue>>();
                results = finalResults;
                foreach (var mutation in mutations)
                {
                    switch (mutation.Type)
                    {
                        case DictionaryMutationType.Add:
                        {
                            if (State.TryGetValue(mutation.Key, out var preExistingValueRef))
                            {
                                if (preExistingValueRef.TryGetTarget(out var preExistingValue))
                                {
                                    throw new AddFailedBecauseKeyAlreadyExistsException(mutation.Key);
                                }
                                else
                                {
                                    State = State.Remove(mutation.Key);
                                }
                            }
                            
                            var value = mutation.ValueIfAdding.Value();
                            State = State.Add(mutation.Key, new WeakReference<TValue>(value));
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateAdd(mutation.Key, true, Maybe<TValue>.Nothing(), value.ToMaybe()));
                            break;
                        }
                        case DictionaryMutationType.TryAdd:
                        {
                            if (State.TryGetValue(mutation.Key, out var preExistingValueRef))
                            {
                                if (preExistingValueRef.TryGetTarget(out var preExistingValue))
                                {
                                    finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateAdd(mutation.Key, false, preExistingValue.ToMaybe(), Maybe<TValue>.Nothing()));
                                }
                                else
                                {
                                    State = State.Remove(mutation.Key);
                                }
                            }
                            
                            var value = mutation.ValueIfAdding.Value();
                            State = State.Add(mutation.Key, new WeakReference<TValue>(value));
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateAdd(mutation.Key, true, Maybe<TValue>.Nothing(), value.ToMaybe()));
                            break;
                        }
                        case DictionaryMutationType.Update:
                        {
                            if (State.TryGetValue(mutation.Key, out var preExistingValue))
                            {
                                if (preExistingValue.TryGetTarget(out var previousValue))
                                {
                                    var newValue = mutation.ValueIfUpdating.Value(previousValue);
                                    State = State.SetItem(mutation.Key, new WeakReference<TValue>(newValue));
                                    finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true, previousValue.ToMaybe(), newValue.ToMaybe()));
                                }
                                else
                                {
                                    throw new KeyNotFoundException();
                                }
                            }
                            break;
                        }
                        case DictionaryMutationType.TryUpdate:
                        {
                            if (State.TryGetValue(mutation.Key, out var preExistingValue))
                            {
                                if (preExistingValue.TryGetTarget(out var previousValue))
                                {
                                    var newValue = mutation.ValueIfUpdating.Value(previousValue);
                                    State = State.SetItem(mutation.Key, new WeakReference<TValue>(newValue));
                                    finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true, previousValue.ToMaybe(), newValue.ToMaybe()));
                                }
                                else
                                {
                                    finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, false, Maybe<TValue>.Nothing(), Maybe<TValue>.Nothing()));
                                }
                            }
                            break;
                        }
                        case DictionaryMutationType.AddOrUpdate:
                        {
                            if (State.TryGetValue(mutation.Key, out var preExistingValue))
                            {
                                if (preExistingValue.TryGetTarget(out var previousValue))
                                {
                                    var newValue = mutation.ValueIfUpdating.Value(previousValue);
                                    State = State.SetItem(mutation.Key, new WeakReference<TValue>(newValue));
                                    finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateAddOrUpdate(mutation.Key, DictionaryItemAddOrUpdateResult.Update, previousValue.ToMaybe(), newValue));
                                    break;
                                }
                                else
                                {
                                    State = State.Remove(mutation.Key);
                                }
                            }

                            var value = mutation.ValueIfAdding.Value();
                            State = State.SetItem(mutation.Key, new WeakReference<TValue>(value));
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateAddOrUpdate(mutation.Key, DictionaryItemAddOrUpdateResult.Add, Maybe<TValue>.Nothing(), value));
                            
                            break;
                        }
                        case DictionaryMutationType.Remove:
                        {
                            if (State.TryGetValue(mutation.Key, out var previousValueRef))
                            {
                                if (previousValueRef.TryGetTarget(out var previousValue))
                                {
                                    State = State.Remove(mutation.Key);
                                    finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key, previousValue.ToMaybe()));
                                    break;
                                }
                                else
                                {
                                    State = State.Remove(mutation.Key);
                                }
                            }

                            throw new KeyNotFoundException();
                            break;
                        }
                        case DictionaryMutationType.TryRemove:
                        {
                            if (State.TryGetValue(mutation.Key, out var previousValueRef))
                            {
                                if (previousValueRef.TryGetTarget(out var previousValue))
                                {
                                    State.Remove(mutation.Key);
                                    finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key, previousValue.ToMaybe()));
                                    break;
                                }
                                else
                                {
                                    State = State.Remove(mutation.Key);
                                }
                            }

                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key, Maybe<TValue>.Nothing()));
                            break;
                        }
                        default:
                            throw new ArgumentException($"Unknown mutation type: {mutation.Type}");
                    }
                }
            }
        }
    }
}