using System;

namespace ComposableCollections.Common
{
    public static class ReadOnlyTransactionalCollection
    {
        public static IReadOnlyTransactionalCollection<TReadOnly> Create<TReadOnly>(Func<TReadOnly> readOnly)
            where TReadOnly : IDisposable
        {
            return new AnonymousReadOnlyTransactionalCollection<TReadOnly>(readOnly);
        }
    }
}