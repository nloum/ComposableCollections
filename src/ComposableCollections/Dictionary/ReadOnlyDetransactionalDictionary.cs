using System;
using System.Collections;
using System.Collections.Generic;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public class ReadOnlyDetransactionalDictionary<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>> _wrapped;

        public ReadOnlyDetransactionalDictionary(IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>> dictionary)
        {
            _wrapped = dictionary;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            var dictionary = _wrapped.BeginRead();
            return new Enumerator<IKeyValue<TKey, TValue>>(dictionary.GetEnumerator(), dictionary);
        }

        public int Count
        {
            get
            {
                using (var dictionary = _wrapped.BeginRead())
                {
                    return dictionary.Count;
                }
            }
        }

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                using (var dictionary = _wrapped.BeginRead())
                {
                    return dictionary.Comparer;
                }
            }
        }
        
        public IEnumerable<TKey> Keys
        {
            get
            {
                var dictionary = _wrapped.BeginRead();
                return new Enumerable<TKey>(() => new Enumerator<TKey>(dictionary.Keys.GetEnumerator(), dictionary));
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                var dictionary = _wrapped.BeginRead();
                return new Enumerable<TValue>(() => new Enumerator<TValue>(dictionary.Values.GetEnumerator(), dictionary));
            }
        }

        public bool ContainsKey(TKey key)
        {
            using (var dictionary = _wrapped.BeginRead())
            {
                return dictionary.ContainsKey(key);
            }
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            using (var dictionary = _wrapped.BeginRead())
            {
                return dictionary.TryGetValue(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            using (var dictionary = _wrapped.BeginRead())
            {
                var result = dictionary.TryGetValue(key);
                if (result.HasValue)
                {
                    value = result.Value;
                    return true;
                }
                else
                {
                    value = default;
                    return false;
                }
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                using (var dictionary = _wrapped.BeginRead())
                {
                    return dictionary[key];
                }
            }
        }

        private class Enumerator<T> : IEnumerator<T>
        {
            private readonly IEnumerator<T> _wrapped;
            private readonly IDisposable _disposable;

            public Enumerator(IEnumerator<T> wrapped, IDisposable disposable)
            {
                _wrapped = wrapped;
                _disposable = disposable;
            }

            public bool MoveNext()
            {
                return _wrapped.MoveNext();
            }

            public void Reset()
            {
                _wrapped.Reset();
            }

            public void Dispose()
            {
                _wrapped.Dispose();
                _disposable.Dispose();
            }

            object IEnumerator.Current => Current;

            public T Current => _wrapped.Current;
        }

        private class Enumerable<T> : IEnumerable<T>
        {
            private readonly Func<IEnumerator<T>> _getEnumerator;

            public Enumerable(Func<IEnumerator<T>> getEnumerator)
            {
                _getEnumerator = getEnumerator;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _getEnumerator();
            }
        }
    }
}