using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using UtilityDisposables;

namespace ComposableCollections.Dictionary.Decorators
{
    public class ReadWriteLockQueryableDictionaryDecorator<TKey, TValue> : LockedDictionaryBase<TKey, TValue>, IQueryableDictionary<TKey, TValue>
    {
        private readonly IQueryableDictionary<TKey, TValue> _source;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private IQueryable<TValue> _values;

        public ReadWriteLockQueryableDictionaryDecorator(IQueryableDictionary<TKey, TValue> source) : base(source)
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

        IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                BeginRead();
                return new Queryable<TValue>(_source.Values, new AnonymousDisposable(() =>
                {
                    EndRead();
                }));
            }
        }
    }
}