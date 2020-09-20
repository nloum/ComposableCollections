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
    public class MappingValuesDictionaryAdapter<TKey, TValue1, TValue2> : MappingKeysAndValuesDictionaryAdapter<TKey, TValue1, TKey, TValue2>, IComposableDictionary<TKey, TValue2>
    {
        public MappingValuesDictionaryAdapter(IComposableDictionary<TKey, TValue1> innerValues) : base(innerValues)
        {
        }

        public MappingValuesDictionaryAdapter(IComposableDictionary<TKey, TValue1> innerValues, Func<TKey, TValue1, TValue2> convertTo2, Func<TKey, TValue2, TValue1> convertTo1) : base(innerValues, (key, value) => new KeyValue<TKey, TValue2>(key, convertTo2(key, value)), (key, value) => new KeyValue<TKey, TValue1>(key, convertTo1(key, value)), null, null)
        {
        }

        public MappingValuesDictionaryAdapter(IComposableDictionary<TKey, TValue1> innerValues, Func<TKey, TValue1, IKeyValue<TKey, TValue2>> convertTo2, Func<TKey, TValue2, IKeyValue<TKey, TValue1>> convertTo1) : base(innerValues, convertTo2, convertTo1, null, null)
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