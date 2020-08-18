using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace ComposableCollections.Dictionary
{
    public class MultiCacheDictionary
    {
        private ReaderWriterLock _lock = new ReaderWriterLock();
        private List<Func<bool, IEnumerable<object>>> _getMutationses = new List<Func<bool, IEnumerable<object>>>();

        public IComposableDictionary<TKey, TValue> Create<TKey, TValue>(IEnumerable<IKeyValue<TKey, TValue>> initialValues)
        {
            _lock.AcquireWriterLock(Int32.MaxValue);
            try
            {
                var result = new Partial<TKey, TValue>(_lock, initialValues);
                _getMutationses.Add(clear => result.GetMutations(clear));
                return result;
            }
            finally
            {
                _lock.ReleaseWriterLock();
            }
        }

        public IEnumerable<object> GetMutations(bool clear)
        {
            _lock.AcquireWriterLock(Int32.MaxValue);
            try
            {
                return _getMutationses.SelectMany(getMutations => getMutations(clear)).ToImmutableList();
            }
            finally
            {
                _lock.ReleaseWriterLock();
            }
        }

        private class Partial<TKey, TValue> : LockedDictionaryDecoratorBase<TKey, TValue>, ICacheDictionary<TKey, TValue>
        {
            private readonly ReaderWriterLock _lock;
            private readonly ICacheDictionary<TKey, TValue> _wrapped;

            public Partial(ReaderWriterLock @lock, IEnumerable<IKeyValue<TKey, TValue>> initialValues) : this(@lock, new ConcurrentCachingDictionary<TKey, TValue>(initialValues.ToComposableDictionary(), new ConcurrentDictionary<TKey, TValue>()))
            {
            }
            
            private Partial(ReaderWriterLock @lock, ICacheDictionary<TKey, TValue> wrapped) : base(wrapped)
            {
                _lock = @lock;
                _wrapped = wrapped;
            }

            public IComposableReadOnlyDictionary<TKey, TValue> AsBypassCache()
            {
                return _wrapped.AsBypassCache();
            }

            public IComposableDictionary<TKey, TValue> AsNeverFlush()
            {
                return _wrapped.AsNeverFlush();
            }

            public void FlushCache()
            {
                _wrapped.FlushCache();
            }

            public IEnumerable<DictionaryMutation<TKey, TValue>> GetMutations(bool clear)
            {
                return _wrapped.GetMutations(clear);
            }

            public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
            {
                return _wrapped.GetEnumerator();
            }
            
            public override IEnumerable<TKey> Keys => _wrapped.Keys;

            public override IEnumerable<TValue> Values => _wrapped.Values;

            protected override void BeginWrite()
            {
                _lock.AcquireReaderLock(Int32.MaxValue);
            }

            protected override void EndWrite()
            {
                _lock.ReleaseReaderLock();
            }

            protected override void BeginRead()
            {
                
            }

            protected override void EndRead()
            {
                
            }
        }
    }
}