using System;
using System.Collections;
using System.Collections.Generic;

namespace ComposableCollections.Utilities
{
    internal class Enumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _source;
        private readonly IDisposable _disposable;

        public Enumerator(IEnumerator<T> source, IDisposable disposable)
        {
            _source = source;
            _disposable = disposable;
        }

        public bool MoveNext()
        {
            return _source.MoveNext();
        }

        public void Reset()
        {
            _source.Reset();
        }

        public void Dispose()
        {
            _source.Dispose();
            _disposable.Dispose();
        }

        object IEnumerator.Current => Current;

        public T Current => _source.Current;
    }
}