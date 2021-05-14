using System;
using System.Collections.Generic;
using System.Linq;
using GenericNumbers.Relational;

namespace ComposableCollections.List
{
    public static class Extensions
    {
        public static IReadOnlyList<T2> Select<T1, T2>(this IReadOnlyList<T1> source, Func<T1, int, T2> selector)
        {
            return new SelectReadOnlyList<T1, T2>(source, selector);
        }
        
        public static TakeReadOnlyList<T> Take<T>(this IReadOnlyList<T> source, int take)
        {
            return new TakeReadOnlyList<T>(source, take);
        }
        
        public static SkipReadOnlyList<T> Skip<T>(this IReadOnlyList<T> source, int skip)
        {
            return new SkipReadOnlyList<T>(source, skip);
        }

        public static IOrderedList<T> OrderBy<T, TKey>(this IList<T> source, Func<T,TKey> keySelector)
        {
            var result = new OrderedList<T>((a, b) => keySelector(a).CompareTo(keySelector(b)));
            foreach (var item in source)
            {
                result.Add(item);
            }

            return result;
        }

        public static IOrderedList<T> OrderBy<T>(this IList<T> source, Func<T,T,int> compare)
        { 
            var result =new OrderedList<T>(compare);
            foreach (var item in source)
            {
                result.Add(item);
            }

            return result;
        }
        
        public static IOrderedList<T> Concat<T>(this IOrderedList<T> a, IOrderedList<T> b)
        {
            var result = new AggregateOrderedList<T> {a, b};
            return result;
        }
        
        public static IOrderedList<T2> SelectMany<T1, T2>(IReadOnlyList<T1> sources, Func<T1, IOrderedList<T2>> selector)
        {
            var result = new AggregateOrderedList<T2>();
            foreach (var source in sources)
            {
                result.Add(selector(source));
            }
            return result;
        }
    }
}