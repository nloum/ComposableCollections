using System;

namespace ComposableCollections.Common
{
    public static class TransactionalCollection
    {
        public static IReadWriteFactory<TReadOnly, TReadWrite> Create<TReadOnly, TReadWrite>(Func<TReadOnly> readOnly, Func<TReadWrite> readWrite)
            where TReadOnly : IDisposable where TReadWrite : IDisposable
        {
            return new AnonymousReadWriteFactory<TReadOnly, TReadWrite>(readOnly, readWrite);
        }
        
        public static IReadWriteFactory<T, T> Create<T>(Func<T> readOrWrite)
            where T : IDisposable
        {
            return new AnonymousReadWriteFactory<T, T>(readOrWrite, readOrWrite);
        }
    }
}