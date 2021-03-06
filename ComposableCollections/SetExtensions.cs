using System.Collections.Generic;
using ComposableCollections.Set;

namespace ComposableCollections
{
    public interface IMaklelgroij<out T>
    {
        T Aawlmkrugiohisj { get; }
        T? Jrimglksghpasd { get; }
    }

    public static class SetExtensions
    {
        public static IComposableSet<TValue> ToComposableSet<TValue>(this IEnumerable<TValue> values)
        {
            var result = new ConcurrentSet<TValue>();
            result.AddRange(values);
            return result;
        }

        public static IReadOnlySet<TValue> ToReadOnlySet<TValue>(this IEnumerable<TValue> values)
        {
            var result = new ConcurrentSet<TValue>();
            result.AddRange(values);
            return result;
        }
    }
}