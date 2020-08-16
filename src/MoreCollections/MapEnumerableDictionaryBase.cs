using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using SimpleMonads;

namespace MoreCollections
{
    /// <summary>
    /// Using two abstract Convert methods, converts an IDictionaryEx{TKey, TInnerValue} to an
    /// IDictionaryEx{TKey, TValue} instance. This will lazily convert objects in the underlying innerValues.
    /// This class works whether innerValues changes underneath it or not.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInnerValue"></typeparam>
    public abstract class MapEnumerableDictionaryBase<TKey, TValue, TInnerValue> : DictionaryBaseEx<TKey, TValue> where TValue : class
    {
        private readonly IDictionaryEx<TKey, TInnerValue> _innerValues;

        protected MapEnumerableDictionaryBase(IDictionaryEx<TKey, TInnerValue> innerValues)
        {
            _innerValues = innerValues;
        }

        protected abstract IEnumerable<IKeyValuePair<TKey, TInnerValue>> Convert(
            IEnumerable<IKeyValuePair<TKey, TValue>> values);

        protected abstract IEnumerable<IKeyValuePair<TKey, TValue>> Convert(
            IEnumerable<IKeyValuePair<TKey, TInnerValue>> innerValues);

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

            value = Convert(new[]{Utility.KeyValuePair(key, innerValue.Value)}).First().Value;
            return true;
        }

        public override IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Convert(_innerValues).GetEnumerator();
        }

        public override int Count => _innerValues.Count;
        public override IEqualityComparer<TKey> Comparer => _innerValues.Comparer;
        public override IEnumerable<TKey> Keys => _innerValues.Keys;
        public override IEnumerable<TValue> Values => this.AsEnumerable().Select(x => x.Value);
        
        public override void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations, out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results)
        {
            var objectsToBeConverted = new Subject<IKeyValuePair<TKey, TValue>>();
            var objectsToBeConvertedBack = new Subject<IKeyValuePair<TKey, TInnerValue>>();
            
            using (var convertedBackObjectsEnumerator = Convert(objectsToBeConvertedBack.ToEnumerable()).GetEnumerator())
            using (var convertedObjectsEnumerator = Convert(objectsToBeConverted.ToEnumerable()).GetEnumerator())
            {
                TInnerValue convert(TKey key, TValue value)
                {
                    objectsToBeConverted.OnNext(Utility.KeyValuePair(key, value));
                    convertedObjectsEnumerator.MoveNext();
                    return convertedObjectsEnumerator.Current.Value;
                }

                TValue convertBack(TKey key, TInnerValue value)
                {
                    objectsToBeConvertedBack.OnNext(Utility.KeyValuePair(key, value));
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
                    return new DictionaryMutation<TKey, TInnerValue>(mutation.Type, mutation.Key, valueIfAdding.ToMaybe(),
                        valueIfUpdating.ToMaybe());
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
        }
    }
}