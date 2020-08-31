using System;

namespace ComposableCollections.Common
{
    public class AnonymousReadOnlyFactory<TReadOnly> : IReadOnlyFactory<TReadOnly> where TReadOnly : IDisposable
    {
        private Func<TReadOnly> _beginRead;

        public AnonymousReadOnlyFactory(Func<TReadOnly> beginRead)
        {
            _beginRead = beginRead;
        }

        public TReadOnly CreateReader()
        {
            return _beginRead();
        }
    }
}