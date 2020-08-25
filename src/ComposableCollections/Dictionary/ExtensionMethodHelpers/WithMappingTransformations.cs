using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithMappingTransformations<TKey1, TValue1, TKey2, TValue2> :
        ComposableDictionaryTransformationsBase<TKey1, TValue1, TKey2, TValue2, Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>>
    {
        public static WithMappingTransformations<TKey1, TValue1, TKey2, TValue2> Default { get; } = new WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>();
        
        private WithMappingTransformations()
        {
        }

        public override IComposableDictionary<TKey2, TValue2> Transform(IComposableDictionary<TKey1, TValue1> source, Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>> p)
        {
            return new MappingDictionaryAdapter<TKey1,TValue1,TKey2,TValue2>(
                source, 
                (key1, value1) => new KeyValue<TKey2, TValue2>(p.Item3(key1), p.Item2(value1)),
                (key2, value2) => new KeyValue<TKey1, TValue1>(p.Item4(key2), p.Item1(value2)),
                p.Item4, p.Item3);
        }

        protected override IComposableReadOnlyDictionary<TKey2, TValue2> Transform(IComposableReadOnlyDictionary<TKey1, TValue1> source, Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>> p)
        {
            return new ReadOnlyMappingDictionaryAdapter<TKey1,TValue1,TKey2,TValue2>(
                source, 
                (key1, value1) => new KeyValue<TKey2, TValue2>(p.Item3(key1), p.Item2(value1)),
                p.Item4, p.Item3);
        }

        protected override IQueryable<TValue2> MapQuery(IQueryable<TValue1> query, Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>> p)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<DictionaryWrite<TKey2, TValue2>> MapWrites(IEnumerable<DictionaryWrite<TKey1, TValue1>> writes, Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>> p)
        {
            foreach (var write in writes)
            {
                yield return new DictionaryWrite<TKey2, TValue2>(write.Type, p.Item3(write.Key),
                    write.ValueIfAdding.Select(valueIfAdding =>
                    {
                        Func<TValue2> result = () =>
                        {
                            return p.Item2(valueIfAdding());
                        };
                        return result;
                    }),
                    write.ValueIfUpdating.Select(valueIfUpdating =>
                    {
                        Func<TValue2, TValue2> result = value => p.Item2(valueIfUpdating(p.Item1(value)));
                        return result;
                    }));
            }
        }
    }
}