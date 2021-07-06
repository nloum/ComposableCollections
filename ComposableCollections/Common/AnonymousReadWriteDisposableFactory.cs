using System;

namespace ComposableCollections.Common
{
    public class AnonymousReadWriteDisposableFactory<TReadOnly, TReadWrite> : AnonymousReadWriteFactory<TReadOnly, TReadWrite>, IDisposableReadWriteFactory<TReadOnly, TReadWrite>  
    {
        private readonly IDisposable _disposable;

        public AnonymousReadWriteDisposableFactory(Func<TReadOnly> beginRead, Func<TReadWrite> beginWrite, IDisposable disposable) : base(beginRead, beginWrite)
        {
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}