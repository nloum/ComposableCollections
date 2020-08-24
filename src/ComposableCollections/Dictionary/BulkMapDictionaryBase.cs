using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Mutations;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    /// <summary>
    /// Using two abstract Convert methods, converts an IDictionaryEx{TKey, TInnerValue} to an
    /// IDictionaryEx{TKey, TValue} instance. This will lazily convert objects in the underlying innerValues.
    /// This class works whether innerValues changes underneath it or not.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInnerValue"></typeparam>
    public abstract class BulkMapDictionaryBase<TKey, TValue, TInnerValue> : DictionaryBase<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TInnerValue> _innerValues;

        protected BulkMapDictionaryBase(IComposableDictionary<TKey, TInnerValue> innerValues)
        {
            _innerValues = innerValues;
        }

        protected abstract IEnumerable<IKeyValue<TKey, TInnerValue>> Convert(
            IEnumerable<IKeyValue<TKey, TValue>> values);

        protected abstract IEnumerable<IKeyValue<TKey, TValue>> Convert(
            IEnumerable<IKeyValue<TKey, TInnerValue>> innerValues);

        public override bool ContainsKey(TKey key)
        {
            return _innerValues.ContainsKey(key);
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            var innerValue = _innerValues.TryGetValue(key);
            if (!innerValue.HasValue)
            {
                value = default;
                return false;
            }

            value = Convert(new[] { new KeyValue<TKey, TInnerValue>(key, innerValue.Value) }).First().Value;
            return true;
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return Convert(_innerValues).GetEnumerator();
        }

        public override int Count => _innerValues.Count;
        public override IEqualityComparer<TKey> Comparer => _innerValues.Comparer;
        public override IEnumerable<TKey> Keys => _innerValues.Keys;
        public override IEnumerable<TValue> Values => this.AsEnumerable().Select(x => x.Value);

        private class InternalEnumerable<TKey, TValue> : IEnumerable<IKeyValue<TKey, TValue>>
        {
            public InternalEnumerator<TKey, TValue> Enumerator { get; } = new InternalEnumerator<TKey, TValue>();

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
            {
                return Enumerator;
            }
        }

        private class InternalEnumerator<TKey, TValue> : IEnumerator<IKeyValue<TKey, TValue>>
        {
            public IKeyValue<TKey, TValue> Next { get; set; }

            public bool MoveNext()
            {
                Current = Next;
                Next = null;
                return Current != null;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public IKeyValue<TKey, TValue> Current { get; private set; }
        }

        public override void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations, out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results)
        {
            var objectsToBeConverted = new InternalEnumerable<TKey, TValue>();
            var objectsToBeConvertedBack = new InternalEnumerable<TKey, TInnerValue>();

            IEnumerator<IKeyValue<TKey, TValue>> convertedBackObjectsEnumerator=null;
            IEnumerator<IKeyValue<TKey, TInnerValue>> convertedObjectsEnumerator=null;

            try
            {
                TInnerValue convert(TKey key, TValue value)
                {
                    objectsToBeConverted.Enumerator.Next = new KeyValue<TKey, TValue>(key, value);

                    if (convertedObjectsEnumerator == null)
                    {
                        convertedObjectsEnumerator = Convert(objectsToBeConverted).GetEnumerator();
                    }

                    convertedObjectsEnumerator.MoveNext();
                    return convertedObjectsEnumerator.Current.Value;
                }

                TValue convertBack(TKey key, TInnerValue value)
                {
                    objectsToBeConvertedBack.Enumerator.Next = new KeyValue<TKey, TInnerValue>(key, value);

                    if (convertedBackObjectsEnumerator == null)
                    {
                        convertedBackObjectsEnumerator = Convert(objectsToBeConvertedBack).GetEnumerator();
                    }

                    convertedBackObjectsEnumerator.MoveNext();
                    return convertedBackObjectsEnumerator.Current.Value;
                }

                _innerValues.Mutate(mutations.Select(mutation =>
                {
                    Func<TInnerValue> valueIfAdding = () =>
                    {
                        var result = mutation.ValueIfAdding.Value();
                        return convert(mutation.Key, result);
                    };
                    Func<TInnerValue, TInnerValue> valueIfUpdating = previousValue =>
                    {
                        var result = mutation.ValueIfAdding.Value();
                        return convert(mutation.Key, result);
                    };
                    return new DictionaryMutation<TKey, TInnerValue>(mutation.Type, mutation.Key, valueIfAdding.ToMaybe(), valueIfUpdating.ToMaybe());
                }), out var innerResults);

                results = innerResults.Select(innerResult =>
                {
                    if (innerResult.Type == DictionaryMutationType.Add || innerResult.Type == DictionaryMutationType.TryAdd)
                    {
                        return DictionaryMutationResult<TKey, TValue>.CreateAdd(innerResult.Key,
                            innerResult.Add.Value.Added,
                            innerResult.Add.Value.ExistingValue.Select(value => convertBack(innerResult.Key, value)),
                            innerResult.Add.Value.NewValue.Select(value => convertBack(innerResult.Key, value)));
                    }
                    else if (innerResult.Type == DictionaryMutationType.Remove || innerResult.Type == DictionaryMutationType.TryRemove)
                    {
                        return DictionaryMutationResult<TKey, TValue>.CreateRemove(innerResult.Key,
                            innerResult.Remove.Value.Select(value => convertBack(innerResult.Key, value)));
                    }
                    else if (innerResult.Type == DictionaryMutationType.Update || innerResult.Type == DictionaryMutationType.TryUpdate)
                    {
                        return DictionaryMutationResult<TKey, TValue>.CreateUpdate(innerResult.Key,
                            innerResult.Update.Value.Updated,
                            innerResult.Update.Value.ExistingValue.Select(value => convertBack(innerResult.Key, value)),
                            innerResult.Update.Value.NewValue.Select(value => convertBack(innerResult.Key, value)));
                    }
                    else if (innerResult.Type == DictionaryMutationType.AddOrUpdate)
                    {
                        return DictionaryMutationResult<TKey, TValue>.CreateAddOrUpdate(innerResult.Key,
                            innerResult.AddOrUpdate.Value.Result,
                            innerResult.AddOrUpdate.Value.ExistingValue.Select(value => convertBack(innerResult.Key, value)),
                            convertBack(innerResult.Key, innerResult.AddOrUpdate.Value.NewValue));
                    }
                    else
                    {
                        throw new InvalidOperationException("Unknown dictionary mutation type");
                    }
                }).ToList();
            }
            finally
            {
                convertedObjectsEnumerator?.Dispose();
                convertedBackObjectsEnumerator?.Dispose();
            }
        }
    }
}