using System;

namespace ComposableCollections.Common
{
    public class AnonymousDisposableReadOnlyFactory<TReadOnly> : AnonymousReadOnlyFactory<TReadOnly>, IDisposableReadOnlyFactory<TReadOnly> 
    {
        private readonly IDisposable _disposable;

        public AnonymousDisposableReadOnlyFactory(Func<TReadOnly> beginRead, IDisposable disposable) : base(beginRead)
        {
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}