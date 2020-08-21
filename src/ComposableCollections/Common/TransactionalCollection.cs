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
    }
}