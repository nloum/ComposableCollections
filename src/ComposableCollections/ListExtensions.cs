using System;
using System.Collections.Generic;
using ComposableCollections.List;
using GenericNumbers;

namespace ComposableCollections
{
    public static class ListExtensions
    {
        public static IReadOnlyList<T> SkipEfficiently<T>(this IReadOnlyList<T> source, int skip)
        {
            return new SkipReadOnlyList<T>(source, skip);
        }

        public static IReadOnlyList<T> TakeEfficiently<T>(this IReadOnlyList<T> source, int take)
        {
            return new TakeReadOnlyList<T>(source, take);
        }

        public static IReadOnlyList<T> TakeEfficiently<T>(this IReadOnlyList<T> source, INumberRange<int> take)
        {
            return new TakeReadOnlyList<T>(new SkipReadOnlyList<T>(source, take.LowerBound.ChangeStrictness(false).Value), take.Size);
        }

        public static IReadOnlyList<TOutput> SelectEfficiently<TInput, TOutput>(this IReadOnlyList<TInput> source, Func<TInput, int, TOutput> selector)
        {
            return new SelectReadOnlyList<TInput, TOutput>(source, selector);
        }

        public static IReadOnlyList<TOutput> SelectEfficiently<TInput, TOutput>(this IReadOnlyList<TInput> source, Func<TInput, TOutput> selector)
        {
            return new SelectReadOnlyList<TInput, TOutput>(source, (item, index) => selector(item));
        }
    }
}