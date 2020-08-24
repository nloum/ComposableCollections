using System;

namespace ComposableCollections.Common
{
    public class AnonymousReadOnlyTransactionalCollection<TReadOnly> : IReadOnlyTransactionalCollection<TReadOnly> where TReadOnly : IDisposable
    {
        private Func<TReadOnly> _beginRead;

        public AnonymousReadOnlyTransactionalCollection(Func<TReadOnly> beginRead)
        {
            _beginRead = beginRead;
        }

        public TReadOnly BeginRead()
        {
            return _beginRead();
        }
    }
}