using System;
using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    public class AnonymousBulkMapDictionary<TKey, TValue, TInnerValue> : BulkMapDictionaryBase<TKey, TValue, TInnerValue>
    {
        private Func<IEnumerable<IKeyValue<TKey, TValue>>, IEnumerable<IKeyValue<TKey, TInnerValue>>> _convert;
        private Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> _convertBack;

        public AnonymousBulkMapDictionary(IComposableDictionary<TKey, TInnerValue> innerValues, Func<IEnumerable<IKeyValue<TKey, TValue>>, IEnumerable<IKeyValue<TKey, TInnerValue>>> convert, Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> convertBack) : base(innerValues)
        {
            _convert = convert;
            _convertBack = convertBack;
        }

        protected override IEnumerable<IKeyValue<TKey, TInnerValue>> Convert(
            IEnumerable<IKeyValue<TKey, TValue>> values)
        {
            return _convert(values);
        }

        protected override IEnumerable<IKeyValue<TKey, TValue>> Convert(
            IEnumerable<IKeyValue<TKey, TInnerValue>> innerValues)
        {
            return _convertBack(innerValues);
        }
    }
}