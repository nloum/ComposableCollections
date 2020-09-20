using System;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    /// <summary>
    /// Using two abstract Convert methods, converts an IDictionaryEx{TKey, TInnerValue} to an
    /// IDictionaryEx{TKey, TValue} instance. This will lazily convert objects in the underlying innerValues.
    /// This class works whether innerValues changes underneath it or not.
    /// </summary>
    public class MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingKeysAndValuesDictionaryAdapter<TKey, TSourceValue, TKey, TValue>, IComposableDictionary<TKey, TValue>
    {
        public MappingValuesDictionaryAdapter(IComposableDictionary<TKey, TSourceValue> innerValues) : base(innerValues)
        {
        }

        public MappingValuesDictionaryAdapter(IComposableDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) : base(innerValues, (key, value) => new KeyValue<TKey, TValue>(key, convertTo2(key, value)), (key, value) => new KeyValue<TKey, TSourceValue>(key, convertTo1(key, value)), null, null)
        {
        }

        public MappingValuesDictionaryAdapter(IComposableDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) : base(innerValues, convertTo2, convertTo1, null, null)
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
}