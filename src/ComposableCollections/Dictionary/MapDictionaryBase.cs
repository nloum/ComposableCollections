using System;
using System.Collections.Generic;
using System.Linq;
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
    public abstract class MapDictionaryBase<TKey, TValue, TInnerValue> : DictionaryBase<TKey, TValue> where TValue : class
    {
        private readonly IComposableDictionary<TKey, TInnerValue> _innerValues;

        protected MapDictionaryBase(IComposableDictionary<TKey, TInnerValue> innerValues)
        {
            _innerValues = innerValues;
        }

        protected abstract TInnerValue Convert(TKey key, TValue value);
        
        protected abstract TValue Convert(TKey key, TInnerValue innerValue);

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

            value = Convert(key, innerValue.Value);
            return true;
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _innerValues.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, Convert(kvp.Key, kvp.Value))).GetEnumerator();
        }

        public override int Count => _innerValues.Count;
        public override IEqualityComparer<TKey> Comparer => _innerValues.Comparer;
        public override IEnumerable<TKey> Keys => _innerValues.Keys;
        public override IEnumerable<TValue> Values => _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value));
        
        public override void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations, out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results)
        {
            _innerValues.Mutate(mutations.Select(mutation =>
            {
                Func<TInnerValue> valueIfAdding = () =>
                {
                    var result = mutation.ValueIfAdding.Value();
                    return Convert(mutation.Key, result);
                };
                Func<TInnerValue, TInnerValue> valueIfUpdating = previousValue =>
                {
                    var result = mutation.ValueIfAdding.Value();
                    return Convert(mutation.Key, result);
                };
                return new DictionaryMutation<TKey, TInnerValue>(mutation.Type, mutation.Key, valueIfAdding.ToMaybe(),
                    valueIfUpdating.ToMaybe());
            }), out var innerResults);

            results = innerResults.Select(innerResult =>
            {
                if (innerResult.Type == DictionaryMutationType.Add || innerResult.Type == DictionaryMutationType.TryAdd)
                {
                    return DictionaryMutationResult<TKey, TValue>.CreateAdd(innerResult.Key,
                        innerResult.Add.Value.Added,
                        innerResult.Add.Value.ExistingValue.Select(value => Convert(innerResult.Key, value)),
                        innerResult.Add.Value.NewValue.Select(value => Convert(innerResult.Key, value)));
                }
                else if (innerResult.Type == DictionaryMutationType.Remove || innerResult.Type == DictionaryMutationType.TryRemove)
                {
                    return DictionaryMutationResult<TKey, TValue>.CreateRemove(innerResult.Key,
                        innerResult.Remove.Value.Select(value => Convert(innerResult.Key, value)));
                }
                else if (innerResult.Type == DictionaryMutationType.Update || innerResult.Type == DictionaryMutationType.TryUpdate)
                {
                    return DictionaryMutationResult<TKey, TValue>.CreateUpdate(innerResult.Key,
                        innerResult.Update.Value.Updated,
                        innerResult.Update.Value.ExistingValue.Select(value => Convert(innerResult.Key, value)),
                        innerResult.Update.Value.NewValue.Select(value => Convert(innerResult.Key, value)));
                }
                else if (innerResult.Type == DictionaryMutationType.AddOrUpdate)
                {
                    return DictionaryMutationResult<TKey, TValue>.CreateAddOrUpdate(innerResult.Key,
                        innerResult.AddOrUpdate.Value.Result,
                        innerResult.AddOrUpdate.Value.ExistingValue.Select(value => Convert(innerResult.Key, value)),
                        Convert(innerResult.Key, innerResult.AddOrUpdate.Value.NewValue));
                }
                else
                {
                    throw new InvalidOperationException("Unknown dictionary mutation type");
                }
            }).ToList();
        }
    }
}