using System;
using ComposableCollections.Common;
using ComposableCollections.Dictionary;

namespace ComposableCollections
{
    public static class CommonExtensions
    {
        public static IReadOnlyFactory<TReadOnly> AsReadOnlyFactory<TReadOnly>(
            this IReadOnlyFactory<TReadOnly> source) where TReadOnly : IDisposable
        {
            return source;
        }
    }
}