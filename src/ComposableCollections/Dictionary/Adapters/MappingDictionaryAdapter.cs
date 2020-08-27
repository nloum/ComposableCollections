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
    public class MappingDictionaryAdapter<TKey, TValue1, TValue2> : MappingDictionaryAdapter<TKey, TValue1, TKey, TValue2>
    {
        public MappingDictionaryAdapter(IComposableDictionary<TKey, TValue1> innerValues) : base(innerValues)
        {
        }

        public MappingDictionaryAdapter(IComposableDictionary<TKey, TValue1> innerValues, Func<TKey, TValue1, TValue2> convertTo2, Func<TKey, TValue2, TValue1> convertTo1) : base(innerValues, (key, value) => new KeyValue<TKey, TValue2>(key, convertTo2(key, value)), (key, value) => new KeyValue<TKey, TValue1>(key, convertTo1(key, value)), null, null)
        {
        }

        public MappingDictionaryAdapter(IComposableDictionary<TKey, TValue1> innerValues, Func<TKey, TValue1, IKeyValue<TKey, TValue2>> convertTo2, Func<TKey, TValue2, IKeyValue<TKey, TValue1>> convertTo1) : base(innerValues, convertTo2, convertTo1, null, null)
        {
        }

        protected override TKey ConvertToKey1(TKey key)
        {
            return key;
        }
        
        protected override TKey ConvertToKey2(TKey key)
        {
            return key;
        }
    }

    /// <summary>
    /// Using two abstract Convert methods, converts an IDictionaryEx{TKey, TInnerValue} to an
    /// IDictionaryEx{TKey, TValue} instance. This will lazily convert objects in the underlying innerValues.
    /// This class works whether innerValues changes underneath it or not.
    /// </summary>
    public class MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2> : DictionaryBase<TKey2, TValue2>
    {
        private readonly IComposableDictionary<TKey1, TValue1> _innerValues;
        private Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> _convertTo2;
        private Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> _convertTo1;
        private Func<TKey2, TKey1> _convertToKey1;
        private Func<TKey1, TKey2> _convertToKey2;
        
        protected MappingDictionaryAdapter(IComposableDictionary<TKey1, TValue1> innerValues)
        {
            _innerValues = innerValues;
        }

        public MappingDictionaryAdapter(IComposableDictionary<TKey1, TValue1> innerValues, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertTo2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertTo1, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TKey1> convertToKey1)
        {
            _innerValues = innerValues;
            _convertTo2 = convertTo2;
            _convertTo1 = convertTo1;
            _convertToKey1 = convertToKey1;
            _convertToKey2 = convertToKey2;
        }

        protected virtual IKeyValue<TKey2, TValue2> Convert(TKey1 key, TValue1 value)
        {
            return _convertTo2(key, value);
        }

        protected virtual IKeyValue<TKey1, TValue1> Convert(TKey2 key, TValue2 value)
        {
            return _convertTo1(key, value);
        }

        protected virtual TKey1 ConvertToKey1(TKey2 key)
        {
            return _convertToKey1(key);
        }

        protected virtual TKey2 ConvertToKey2(TKey1 key)
        {
            return _convertToKey2(key);
        }

        public override bool ContainsKey(TKey2 key)
        {
            return _innerValues.ContainsKey(ConvertToKey1(key));
        }

        public override bool TryGetValue(TKey2 key, out TValue2 value)
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

        public override IEnumerator<IKeyValue<TKey2, TValue2>> GetEnumerator()
        {
            return _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => _innerValues.Count;
        public override IEqualityComparer<TKey2> Comparer => EqualityComparer<TKey2>.Default;
        public override IEnumerable<TKey2> Keys => _innerValues.Keys.Select(ConvertToKey2);
        public override IEnumerable<TValue2> Values => _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value).Value);

        public override void Write(IEnumerable<DictionaryWrite<TKey2, TValue2>> writes,
            out IReadOnlyList<DictionaryWriteResult<TKey2, TValue2>> results)
        {
            _innerValues.Write(writes.Select(write =>
            {
                Func<TValue1> valueIfAdding = () =>
                {
                    var result = write.ValueIfAdding.Value();
                    return Convert(write.Key, result).Value;
                };
                Func<TValue1, TValue1> valueIfUpdating = previousValue =>
                {
                    var result = write.ValueIfAdding.Value();
                    return Convert(write.Key, result).Value;
                };
                return new DictionaryWrite<TKey1, TValue1>(write.Type, ConvertToKey1(write.Key),
                    valueIfAdding.ToMaybe(),
                    valueIfUpdating.ToMaybe());
            }), out var innerResults);

            results = innerResults.Select(innerResult =>
            {
                if (innerResult.Type == DictionaryWriteType.Add || innerResult.Type == DictionaryWriteType.TryAdd)
                {
                    return DictionaryWriteResult<TKey2, TValue2>.CreateAdd(ConvertToKey2(innerResult.Key),
                        innerResult.Add.Value.Added,
                        innerResult.Add.Value.ExistingValue.Select(value => Convert(innerResult.Key, value).Value),
                        innerResult.Add.Value.NewValue.Select(value => Convert(innerResult.Key, value).Value));
                }
                else if (innerResult.Type == DictionaryWriteType.Remove ||
                         innerResult.Type == DictionaryWriteType.TryRemove)
                {
                    return DictionaryWriteResult<TKey2, TValue2>.CreateRemove(ConvertToKey2(innerResult.Key),
                        innerResult.Remove.Value.Select(value => Convert(innerResult.Key, value).Value));
                }
                else if (innerResult.Type == DictionaryWriteType.Update ||
                         innerResult.Type == DictionaryWriteType.TryUpdate)
                {
                    return DictionaryWriteResult<TKey2, TValue2>.CreateUpdate(ConvertToKey2(innerResult.Key),
                        innerResult.Update.Value.Updated,
                        innerResult.Update.Value.ExistingValue.Select(value => Convert(innerResult.Key, value).Value),
                        innerResult.Update.Value.NewValue.Select(value => Convert(innerResult.Key, value).Value));
                }
                else if (innerResult.Type == DictionaryWriteType.AddOrUpdate)
                {
                    return DictionaryWriteResult<TKey2, TValue2>.CreateAddOrUpdate(ConvertToKey2(innerResult.Key),
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