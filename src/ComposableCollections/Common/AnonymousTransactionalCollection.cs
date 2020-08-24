using System;

namespace ComposableCollections.Common
{
    public class AnonymousTransactionalCollection<TReadOnly, TReadWrite> : ITransactionalCollection<TReadOnly, TReadWrite> where TReadOnly : IDisposable where TReadWrite : IDisposable
    {
        private Func<TReadOnly> _beginRead;
        private Func<TReadWrite> _beginWrite;
        
        public AnonymousTransactionalCollection(Func<TReadOnly> beginRead, Func<TReadWrite> beginWrite)
        {
            _beginRead = beginRead;
            _beginWrite = beginWrite;
        }

        public TReadOnly BeginRead()
        {
            return _beginRead();
        }

        public TReadWrite BeginWrite()
        {
            return _beginWrite();
        }
    }
}