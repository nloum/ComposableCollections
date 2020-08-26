using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithMappingTransformations<TKey1, TValue1, TKey2, TValue2> {
        public static IComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<Func<TValue2, TValue1>,
            Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>> ComposableDictionaryTransformations { get; }
        public static IDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<Tuple<Func<TValue2, TValue1>,
            Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>, Func<TValue2, TKey2>>> DictionaryWithBuiltInKeyTransformations { get; }
        public static IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<Func<TValue2, TValue1>,
            Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>> ComposableReadOnlyDictionaryTransformations { get; }
        public static IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<Tuple<Func<TValue2, TValue1>,
            Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>, Func<TValue2, TKey2>>> ReadOnlyDictionaryWithBuiltInKeyTransformations { get; }
        public static ITransactionalTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<Func<TValue2, TValue1>,
            Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>> TransactionalTransformations { get; }
        public static ITransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, Tuple<Tuple<Func<TValue2, TValue1>,
            Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>, Func<TValue2, TKey2>>> TransactionalTransformationsWithBuiltInKey { get; }

        static WithMappingTransformations()
        {
            var composableDictionaryTransformation = new ComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<Func<TValue2, TValue1>,
                Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>>(Transform, Transform, MapQuery, MapWrites);
            ComposableDictionaryTransformations = composableDictionaryTransformation;
            DictionaryWithBuiltInKeyTransformations = composableDictionaryTransformation;
            ComposableReadOnlyDictionaryTransformations =
                new ComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<
                    Func<TValue2, TValue1>,
                    Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>>(Transform,
                    ConvertQueryable);
            ReadOnlyDictionaryWithBuiltInKeyTransformations =
                new ReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<Tuple<
                    Func<TValue2, TValue1>,
                    Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>, Func<TValue2, TKey2>>>(Transform,
                    ConvertGetKey, ConvertQueryable);
            TransactionalTransformations = new TransactionalTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>>(
                ComposableReadOnlyDictionaryTransformations, ComposableDictionaryTransformations);
            TransactionalTransformationsWithBuiltInKey = new TransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, Tuple<Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>, Func<TValue2, TKey2>>>(
                ReadOnlyDictionaryWithBuiltInKeyTransformations, DictionaryWithBuiltInKeyTransformations);
        }

        private static IComposableReadOnlyDictionary<TKey2, TValue2> Transform(IComposableReadOnlyDictionary<TKey1, TValue1> arg1, Tuple<Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>, Func<TValue2, TKey2>> arg2)
        {
            return Transform(arg1, arg2.Item1);
        }

        private static IQueryable<TValue2> ConvertQueryable(IQueryable<TValue1> arg1, Tuple<Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>, Func<TValue2, TKey2>> arg2)
        {
            return ConvertQueryable(arg1, arg2.Item1);
        }

        private static Func<TValue2, TKey2> ConvertGetKey(Func<TValue1, TKey1> arg1, Tuple<Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>>, Func<TValue2, TKey2>> arg2)
        {
            return arg2.Item2;
        }

        private static IQueryable<TValue2> ConvertQueryable(IQueryable<TValue1> arg1, Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>> arg2)
        {
            throw new NotImplementedException();
        }

        private static IComposableDictionary<TKey2, TValue2> Transform(IComposableDictionary<TKey1, TValue1> source,
            Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>> p)
        {
            return new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(
                source,
                (key1, value1) => new KeyValue<TKey2, TValue2>(p.Item3(key1), p.Item2(value1)),
                (key2, value2) => new KeyValue<TKey1, TValue1>(p.Item4(key2), p.Item1(value2)),
                p.Item4, p.Item3);
        }

        private static IComposableReadOnlyDictionary<TKey2, TValue2> Transform(
            IComposableReadOnlyDictionary<TKey1, TValue1> source,
            Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>> p)
        {
            return new ReadOnlyMappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(
                source,
                (key1, value1) => new KeyValue<TKey2, TValue2>(p.Item3(key1), p.Item2(value1)),
                p.Item4, p.Item3);
        }

        private static IQueryable<TValue2> MapQuery(IQueryable<TValue1> query,
            Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>> p)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<DictionaryWrite<TKey2, TValue2>> MapWrites(
            IEnumerable<DictionaryWrite<TKey1, TValue1>> writes,
            Tuple<Func<TValue2, TValue1>, Func<TValue1, TValue2>, Func<TKey1, TKey2>, Func<TKey2, TKey1>> p)
        {
            foreach (var write in writes)
            {
                yield return new DictionaryWrite<TKey2, TValue2>(write.Type, p.Item3(write.Key),
                    write.ValueIfAdding.Select(valueIfAdding =>
                    {
                        Func<TValue2> result = () => { return p.Item2(valueIfAdding()); };
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