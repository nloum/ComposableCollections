using System;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class MappingValuesReadOnlyDictionaryAdapter<TKey, TValue1, TValue2> : MappingKeysAndValuesReadOnlyDictionaryAdapter<TKey, TValue1, TKey, TValue2>, IComposableReadOnlyDictionary<TKey, TValue2>
    {
        protected MappingValuesReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TValue1> innerValues) : base(innerValues)
        {
        }

        public MappingValuesReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TValue1> innerValues, Func<TKey, TValue1, IKeyValue<TKey, TValue2>> convertTo2) : base(innerValues, convertTo2, x => x, x => x)
        {
        }
    }
}