using System;

namespace ComposableCollections.Common
{
    public static class ReadOnlyTransactionalCollection
    {
        public static IReadOnlyFactory<TReadOnly> Create<TReadOnly>(Func<TReadOnly> readOnly)
            where TReadOnly : IDisposable
        {
            return new AnonymousReadOnlyFactory<TReadOnly>(readOnly);
        }
    }
}