using System;
using System.Threading;
using ComposableCollections.Common;
using UtilityDisposables;

namespace ComposableCollections.Dictionary.Transactional
{
    public class ReadWriteLockTransactionalDecorator<TReadOnly, TReadWrite> : IReadWriteFactory<TReadOnly, TReadWrite>  
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly IReadWriteFactory<TReadOnly, TReadWrite> _transactionalDictionary;
        private readonly Func<TReadOnly, IDisposable, TReadOnly> _addReadOnlyDisposable;
        private readonly Func<TReadWrite, IDisposable, TReadWrite> _addReadWriteDisposable;

        public ReadWriteLockTransactionalDecorator(IReadWriteFactory<TReadOnly, TReadWrite> transactionalDictionary, Func<TReadOnly, IDisposable, TReadOnly> addReadOnlyDisposable, Func<TReadWrite, IDisposable, TReadWrite> addReadWriteDisposable)
        {
            _transactionalDictionary = transactionalDictionary;
            _addReadOnlyDisposable = addReadOnlyDisposable;
            _addReadWriteDisposable = addReadWriteDisposable;
        }

        public TReadOnly CreateReader()
        {
            _lock.EnterReadLock();
            var result = _transactionalDictionary.CreateReader();
            return _addReadOnlyDisposable(result, new AnonymousDisposable(() => _lock.ExitReadLock()));
        }

        public TReadWrite CreateWriter()
        {
            _lock.EnterWriteLock();
            var result = _transactionalDictionary.CreateWriter();
            return _addReadWriteDisposable(result, new AnonymousDisposable(() => _lock.ExitWriteLock()));
        }
    }
}