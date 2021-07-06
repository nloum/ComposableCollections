using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using UtilityDisposables;

namespace ComposableCollections.Dictionary.Decorators
{
    public class ReadWriteLockDictionaryDecorator<TKey, TValue> : LockedDictionaryBase<TKey, TValue>, IComposableDictionary<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TValue> _source;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        
        public ReadWriteLockDictionaryDecorator(IComposableDictionary<TKey, TValue> source) : base(source)
        {
            _source = source;
        }

        protected override void BeginWrite()
        {
            _lock.EnterWriteLock();
        }

        protected override void EndWrite()
        {
            _lock.ExitWriteLock();
        }

        protected override void BeginRead()
        {
            _lock.EnterReadLock();
        }

        protected override void EndRead()
        {
            _lock.ExitReadLock();
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            BeginRead();
            return new Enumerator<IKeyValue<TKey, TValue>>(_source.GetEnumerator(), new AnonymousDisposable(() =>
            {
                EndRead();
            }));
        }

        public override IEnumerable<TKey> Keys
        {
            get
            {
                BeginRead();
                return new Enumerable<TKey>(() =>
                {
                    return new Enumerator<TKey>(_source.Keys.GetEnumerator(), new AnonymousDisposable(() =>
                    {
                        EndRead();
                    }));
                });
            }
        }

        public override IEnumerable<TValue> Values
        {
            get
            {
                BeginRead();
                return new Enumerable<TValue>(() =>
                {
                    return new Enumerator<TValue>(_source.Values.GetEnumerator(), new AnonymousDisposable(() =>
                    {
                        EndRead();
                    }));
                });
            }
        }
    }
}