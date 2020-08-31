using System;

namespace ComposableCollections.Common
{
    public class AnonymousReadWriteFactory<TReadOnly, TReadWrite> : IReadWriteFactory<TReadOnly, TReadWrite> where TReadOnly : IDisposable where TReadWrite : IDisposable
    {
        private Func<TReadOnly> _beginRead;
        private Func<TReadWrite> _beginWrite;
        
        public AnonymousReadWriteFactory(Func<TReadOnly> beginRead, Func<TReadWrite> beginWrite)
        {
            _beginRead = beginRead;
            _beginWrite = beginWrite;
        }

        public TReadOnly CreateReader()
        {
            return _beginRead();
        }

        public TReadWrite CreateWriter()
        {
            return _beginWrite();
        }
    }
}