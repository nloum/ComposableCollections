using System;
using System.Threading;
using ComposableCollections.Common;
using UtilityDisposables;

namespace ComposableCollections.Dictionary.Transactional
{
    public class ReadWriteLockTransactionalDecorator<TReadOnly, TReadWrite> : ITransactionalCollection<TReadOnly, TReadWrite> where TReadOnly : IDisposable where TReadWrite : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly ITransactionalCollection<TReadOnly, TReadWrite> _transactionalDictionary;
        private readonly Func<TReadOnly, IDisposable, TReadOnly> _addReadOnlyDisposable;
        private readonly Func<TReadWrite, IDisposable, TReadWrite> _addReadWriteDisposable;

        public ReadWriteLockTransactionalDecorator(ITransactionalCollection<TReadOnly, TReadWrite> transactionalDictionary, Func<TReadOnly, IDisposable, TReadOnly> addReadOnlyDisposable, Func<TReadWrite, IDisposable, TReadWrite> addReadWriteDisposable)
        {
            _transactionalDictionary = transactionalDictionary;
            _addReadOnlyDisposable = addReadOnlyDisposable;
            _addReadWriteDisposable = addReadWriteDisposable;
        }

        public TReadOnly BeginRead()
        {
            _lock.EnterReadLock();
            var result = _transactionalDictionary.BeginRead();
            return _addReadOnlyDisposable(result, new AnonymousDisposable(() => _lock.ExitReadLock()));
        }

        public TReadWrite BeginWrite()
        {
            _lock.EnterWriteLock();
            var result = _transactionalDictionary.BeginWrite();
            return _addReadWriteDisposable(result, new AnonymousDisposable(() => _lock.ExitWriteLock()));
        }
    }
}