using System;
using System.Collections;
using System.Collections.Generic;

namespace ComposableCollections.Utilities
{
    internal class Enumerable<T> : IEnumerable<T>
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