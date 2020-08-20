using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousDisposableReadOnlyTransactionalCollection<TReadOnly> : AnonymousReadOnlyTransactionalCollection<TReadOnly>, IDisposableReadOnlyTransactionalCollection<TReadOnly> where TReadOnly : IDisposable
    {
        private readonly IDisposable _disposable;

        public AnonymousDisposableReadOnlyTransactionalCollection(Func<TReadOnly> beginRead, IDisposable disposable) : base(beginRead)
        {
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}