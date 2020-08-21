using System;

namespace ComposableCollections.Dictionary
{
    public static class TransactionalCollection
    {
        public static ITransactionalCollection<TReadOnly, TReadWrite> Create<TReadOnly, TReadWrite>(Func<TReadOnly> readOnly, Func<TReadWrite> readWrite)
            where TReadOnly : IDisposable where TReadWrite : IDisposable
        {
            return new AnonymousTransactionalCollection<TReadOnly, TReadWrite>(readOnly, readWrite);
        }
        
        public static ITransactionalCollection<T, T> Create<T>(Func<T> readOrWrite)
            where T : IDisposable
        {
            return new AnonymousTransactionalCollection<T, T>(readOrWrite, readOrWrite);
        }
    }
}