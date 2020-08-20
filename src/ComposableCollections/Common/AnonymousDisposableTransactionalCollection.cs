using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousDisposableTransactionalCollection<TReadOnly, TReadWrite> : AnonymousTransactionalCollection<TReadOnly, TReadWrite>, IDisposableTransactionalCollection<TReadOnly, TReadWrite> where TReadOnly : IDisposable where TReadWrite : IDisposable
    {
        private readonly IDisposable _disposable;

        public AnonymousDisposableTransactionalCollection(Func<TReadOnly> beginRead, Func<TReadWrite> beginWrite, IDisposable disposable) : base(beginRead, beginWrite)
        {
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}