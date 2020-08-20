using System;
using ComposableCollections.Dictionary;

namespace ComposableCollections
{
    public static class CommonExtensions
    {
        public static IReadOnlyTransactionalCollection<TReadOnly> AsReadOnlyTransactionalCollection<TReadOnly>(
            this IReadOnlyTransactionalCollection<TReadOnly> source) where TReadOnly : IDisposable
        {
            return source;
        }
    }
}