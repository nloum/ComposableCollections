using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Exceptions;
using ComposableCollections.Dictionary.Write;
using DataStructures.Trees;
using GenericNumbers.Relational;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Sources
{
    public class OrderedDictionary<TKey, TValue> : DictionaryBase<TKey, TValue>
    {
        private RedBlackTree<KeyValueOrderedByKey<TKey, TValue>> _state = new RedBlackTree<KeyValueOrderedByKey<TKey, TValue>>();
        private readonly Func<IKeyValue<TKey, TValue>, IKeyValue<TKey, TValue>, int> _compareKeysAndValues;
        private readonly Func<TKey, TKey, int> _compareKeys;

        public OrderedDictionary(Func<IKeyValue<TKey, TValue>, IKeyValue<TKey, TValue>, int> compareKeysAndValues, Func<TKey, TKey, int> compareKeys)
        {
            _compareKeysAndValues = compareKeysAndValues;
            _compareKeys = compareKeys;
            Comparer = EqualityComparer<TKey>.Default;
        }

        public OrderedDictionary() : this((kvp1, kvp2) => kvp1.Key.CompareTo(kvp2.Key), (key1, key2) => key1.CompareTo(key2))
        {
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            var result = _state.Find(new KeyValueOrderedByKey<TKey, TValue>(key, _compareKeys));
            if (result != null)
            {
                value = result.Value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _state.GetInOrderEnumerator();
        }

        public override int Count => _state.Count;
        public override IEqualityComparer<TKey> Comparer { get; }
        public override IEnumerable<TKey> Keys => this.Select(kvp => kvp.Key);
        public override IEnumerable<TValue> Values => this.Select(kvp => kvp.Value);
        public override void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            var finalResult = new List<DictionaryWriteResult<TKey, TValue>>();
            
            foreach (var write in writes)
            {
                switch (write.Type)
                {
                    case DictionaryWriteType.Add:
                    {
                        if (TryGetValue(write.Key, out var existingValue))
                        {
                            throw new AddFailedBecauseKeyAlreadyExistsException(write.Key);
                        }

                        var newValue = write.ValueIfAdding.Value();
                        _state.Insert(new KeyValueOrderedByKey<TKey, TValue>(write.Key, newValue, _compareKeysAndValues));
                        finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, true,
                            Maybe<TValue>.Nothing(), newValue.ToMaybe()));

                        break;
                    }
                    case DictionaryWriteType.TryAdd:
                    {
                        if (TryGetValue(write.Key, out var existingValue))
                        {
                            finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, false,
                                existingValue.ToMaybe(), Maybe<TValue>.Nothing()));
                        }
                        else
                        {
                            var newValue = write.ValueIfAdding.Value();
                            _state.Insert(new KeyValueOrderedByKey<TKey, TValue>(write.Key, newValue, _compareKeysAndValues));
                            finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, true,
                                Maybe<TValue>.Nothing(), newValue.ToMaybe()));
                        }

                        break;
                    }
                    case DictionaryWriteType.Update:
                    {
                        if (!TryGetValue(write.Key, out var existingValue))
                        {
                            throw new UpdateFailedBecauseNoSuchKeyExistsException(write.Key);
                        }

                        var newValue = write.ValueIfUpdating.Value(existingValue);
                        _state.Insert(new KeyValueOrderedByKey<TKey, TValue>(write.Key, newValue, _compareKeysAndValues));
                        finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true,
                            existingValue.ToMaybe(), newValue.ToMaybe()));

                        break;
                    }
                    case DictionaryWriteType.TryUpdate:
                    {
                        if (!TryGetValue(write.Key, out var existingValue))
                        {
                            finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, false,
                                Maybe<TValue>.Nothing(), Maybe<TValue>.Nothing()));
                        }
                        else
                        {
                            var newValue = write.ValueIfUpdating.Value(existingValue);
                            _state.Insert(new KeyValueOrderedByKey<TKey, TValue>(write.Key, newValue, _compareKeysAndValues));
                            finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true,
                                existingValue.ToMaybe(), newValue.ToMaybe()));
                        }

                        break;
                    }
                    case DictionaryWriteType.AddOrUpdate:
                    {
                        if (!TryGetValue(write.Key, out var existingValue))
                        {
                            var newValue = write.ValueIfAdding.Value();
                            _state.Insert(new KeyValueOrderedByKey<TKey, TValue>(write.Key, newValue, _compareKeysAndValues));
                            finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateAddOrUpdate(write.Key, DictionaryItemAddOrUpdateResult.Add,
                                Maybe<TValue>.Nothing(), newValue));
                        }
                        else
                        {
                            var newValue = write.ValueIfUpdating.Value(existingValue);
                            _state.Insert(new KeyValueOrderedByKey<TKey, TValue>(write.Key, newValue, _compareKeysAndValues));
                            finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateAddOrUpdate(write.Key, DictionaryItemAddOrUpdateResult.Update,
                                existingValue.ToMaybe(), newValue));
                        }

                        break;
                    }
                    case DictionaryWriteType.Remove:
                    {
                        if (!TryGetValue(write.Key, out var existingValue))
                        {
                            throw new RemoveFailedBecauseNoSuchKeyExistsException(write.Key);
                        }

                        _state.Remove(new KeyValueOrderedByKey<TKey, TValue>(write.Key, existingValue, _compareKeysAndValues));
                        finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, existingValue.ToMaybe()));

                        break;
                    }
                    case DictionaryWriteType.TryRemove:
                    {
                        if (!TryGetValue(write.Key, out var existingValue))
                        {
                            finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, Maybe<TValue>.Nothing()));
                        }
                        else
                        {
                            _state.Remove(new KeyValueOrderedByKey<TKey, TValue>(write.Key, existingValue, _compareKeysAndValues));
                            finalResult.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, existingValue.ToMaybe()));
                        }

                        break;
                    }
                    default:
                        throw new ArgumentException($"Unknown dictionary write type {write.Type}");
                }
            }

            results = finalResult;
        }

        private class AnonymousEqualityComparer : IEqualityComparer<IKeyValue<TKey, TValue>>
        {
            private Func<IKeyValue<TKey, TValue>, IKeyValue<TKey, TValue>, bool> _equals;

            public AnonymousEqualityComparer(Func<IKeyValue<TKey, TValue>, IKeyValue<TKey, TValue>, bool> @equals)
            {
                _equals = @equals;
            }

            public bool Equals(IKeyValue<TKey, TValue> x, IKeyValue<TKey, TValue> y)
            {
                return _equals(x, y);
            }

            public int GetHashCode(IKeyValue<TKey, TValue> obj)
            {
                return obj.GetHashCode();
            }
        }
        
        private class KeyValueOrderedByKey<TKey, TValue> : IKeyValue<TKey, TValue>, IComparable<IKeyValue<TKey, TValue>>
        {
            readonly private Func<IKeyValue<TKey, TValue>, IKeyValue<TKey, TValue>, int> _compareTo;
            public KeyValueOrderedByKey(TKey key, TValue value, Func<IKeyValue<TKey, TValue>, IKeyValue<TKey, TValue>, int> compareTo)
            {
                Key = key;
                Value = value;
                _compareTo = compareTo;
            }

            public KeyValueOrderedByKey(TKey key, Func<TKey, TKey, int> compareTo)
            {
                Key = key;
                _compareTo = (kvp1, kvp2) => compareTo(kvp1.Key, kvp2.Key);
            }

            public TKey Key { get; }
            public TValue Value { get; }
            public int CompareTo(IKeyValue<TKey, TValue> other)
            {
                return _compareTo(this, other);
            }
        }
    }
}