using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

namespace ComposableCollections.Dictionary
{
    public class DictionaryReadWriteLockSlimDecorator<TKey, TValue> : LockedDictionaryBase<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TValue> _wrapped;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        
        public DictionaryReadWriteLockSlimDecorator(IComposableDictionary<TKey, TValue> wrapped) : base(wrapped)
        {
            _wrapped = wrapped;
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
                var result = _wrapped.ToImmutableList().GetEnumerator();
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
                    var result = _wrapped.Keys.ToImmutableList();
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
                    var result = _wrapped.Values.ToImmutableList();
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