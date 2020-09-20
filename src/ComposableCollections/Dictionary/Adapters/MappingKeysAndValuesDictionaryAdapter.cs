using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Adapters
{
    /// <summary>
    /// Using two abstract Convert methods, converts an IDictionaryEx{TKey, TInnerValue} to an
    /// IDictionaryEx{TKey, TValue} instance. This will lazily convert objects in the underlying innerValues.
    /// This class works whether innerValues changes underneath it or not.
    /// </summary>
    public class MappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : DictionaryBase<TKey, TValue>, IComposableDictionary<TKey, TValue>
    {
        private readonly IComposableDictionary<TSourceKey, TSourceValue> _innerValues;
        private Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> _convertTo2;
        private Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> _convertTo1;
        private Func<TKey, TSourceKey> _convertToKey1;
        private Func<TSourceKey, TKey> _convertToKey2;
        
        protected MappingKeysAndValuesDictionaryAdapter(IComposableDictionary<TSourceKey, TSourceValue> innerValues)
        {
            _innerValues = innerValues;
        }

        public MappingKeysAndValuesDictionaryAdapter(IComposableDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1)
        {
            _innerValues = innerValues;
            _convertTo2 = convertTo2;
            _convertTo1 = convertTo1;
            _convertToKey1 = convertToKey1;
            _convertToKey2 = convertToKey2;
        }

        protected virtual IKeyValue<TKey, TValue> Convert(TSourceKey key, TSourceValue value)
        {
            return _convertTo2(key, value);
        }

        protected virtual IKeyValue<TSourceKey, TSourceValue> Convert(TKey key, TValue value)
        {
            return _convertTo1(key, value);
        }

        protected virtual TSourceKey ConvertToKey1(TKey key)
        {
            return _convertToKey1(key);
        }

        protected virtual TKey ConvertToKey2(TSourceKey key)
        {
            return _convertToKey2(key);
        }

        public override bool ContainsKey(TKey key)
        {
            return _innerValues.ContainsKey(ConvertToKey1(key));
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            var convertedKey = ConvertToKey1(key);
            var innerValue = _innerValues.TryGetValue(convertedKey);
            if (!innerValue.HasValue)
            {
                value = default;
                return false;
            }

            var kvp = Convert(convertedKey, innerValue.Value);
            value = kvp.Value;
            return true;
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _innerValues.Select(kvp => Convert((TSourceKey) kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => _innerValues.Count;
        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;
        public override IEnumerable<TKey> Keys => _innerValues.Keys.Select(ConvertToKey2);
        public override IEnumerable<TValue> Values => _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value).Value);

        public override void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes,
            out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            _innerValues.Write(writes.Select(write =>
            {
                Func<TSourceValue> valueIfAdding = () =>
                {
                    var result = write.ValueIfAdding.Value();
                    return Convert(write.Key, result).Value;
                };
                Func<TSourceValue, TSourceValue> valueIfUpdating = previousValue =>
                {
                    var result = write.ValueIfAdding.Value();
                    return Convert(write.Key, result).Value;
                };
                return new DictionaryWrite<TSourceKey, TSourceValue>(write.Type, ConvertToKey1(write.Key),
                    valueIfAdding.ToMaybe(),
                    valueIfUpdating.ToMaybe());
            }), out var innerResults);

            results = innerResults.Select(innerResult =>
            {
                if (innerResult.Type == DictionaryWriteType.Add || innerResult.Type == DictionaryWriteType.TryAdd)
                {
                    return DictionaryWriteResult<TKey, TValue>.CreateAdd(ConvertToKey2(innerResult.Key),
                        innerResult.Add.Value.Added,
                        innerResult.Add.Value.ExistingValue.Select(value => Convert(innerResult.Key, value).Value),
                        innerResult.Add.Value.NewValue.Select(value => Convert(innerResult.Key, value).Value));
                }
                else if (innerResult.Type == DictionaryWriteType.Remove ||
                         innerResult.Type == DictionaryWriteType.TryRemove)
                {
                    return DictionaryWriteResult<TKey, TValue>.CreateRemove(ConvertToKey2(innerResult.Key),
                        innerResult.Remove.Value.Select(value => Convert(innerResult.Key, value).Value));
                }
                else if (innerResult.Type == DictionaryWriteType.Update ||
                         innerResult.Type == DictionaryWriteType.TryUpdate)
                {
                    return DictionaryWriteResult<TKey, TValue>.CreateUpdate(ConvertToKey2(innerResult.Key),
                        innerResult.Update.Value.Updated,
                        innerResult.Update.Value.ExistingValue.Select(value => Convert(innerResult.Key, value).Value),
                        innerResult.Update.Value.NewValue.Select(value => Convert(innerResult.Key, value).Value));
                }
                else if (innerResult.Type == DictionaryWriteType.AddOrUpdate)
                {
                    return DictionaryWriteResult<TKey, TValue>.CreateAddOrUpdate(ConvertToKey2(innerResult.Key),
                        innerResult.AddOrUpdate.Value.Result,
                        innerResult.AddOrUpdate.Value.ExistingValue.Select(value =>
                            Convert(innerResult.Key, value).Value),
                        Convert(innerResult.Key, innerResult.AddOrUpdate.Value.NewValue).Value);
                }
                else
                {
                    throw new InvalidOperationException("Unknown dictionary write type");
                }
            }).ToList();
        }
    }
}