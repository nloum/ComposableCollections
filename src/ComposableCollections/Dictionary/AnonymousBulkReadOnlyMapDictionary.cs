using System;
using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    public class AnonymousBulkReadOnlyMapDictionary<TKey, TValue, TInnerValue> : BulkReadOnlyMapDictionaryBase<TKey, TValue, TInnerValue>
    {
        private Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> _convertBack;

        public AnonymousBulkReadOnlyMapDictionary(IComposableReadOnlyDictionary<TKey, TInnerValue> innerValues, Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> convertBack) : base(innerValues)
        {
            _convertBack = convertBack;
        }

        protected override IEnumerable<IKeyValue<TKey, TValue>> Convert(
            IEnumerable<IKeyValue<TKey, TInnerValue>> innerValues)
        {
            return _convertBack(innerValues);
        }
    }
}