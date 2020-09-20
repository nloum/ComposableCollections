using System;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue> : MappingKeysAndValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TKey, TValue>, IComposableReadOnlyDictionary<TKey, TValue>
    {
        protected MappingValuesReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TSourceValue> innerValues) : base(innerValues)
        {
        }

        public MappingValuesReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) : base(innerValues, convertTo2, x => x, x => x)
        {
        }
    }
}