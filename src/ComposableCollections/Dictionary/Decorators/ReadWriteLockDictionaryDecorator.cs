using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Decorators
{
    public class ReadWriteLockDictionaryDecorator<TKey, TValue> : LockedDictionaryBase<TKey, TValue>
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
            try
            {
                var result = _source.ToImmutableList().GetEnumerator();
                return result;
            }
            finally
            {
                EndRead();
            }
        }

        public override IEnumerable<TKey> Keys
        {
            get
            {
                BeginRead();
                try
                {
                    var result = _source.Keys.ToImmutableList();
                    return result;
                }
                finally
                {
                    EndRead();
                }
            }
        }

        public override IEnumerable<TValue> Values
        {
            get
            {
                BeginRead();
                try
                {
                    var result = _source.Values.ToImmutableList();
                    return result;
                }
                finally
                {
                    EndRead();
                }
            }
        }
    }
}