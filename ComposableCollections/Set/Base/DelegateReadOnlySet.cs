using System.Collections;
using System.Collections.Generic;

namespace ComposableCollections.Set.Base
{
    public class DelegateReadOnlySet<TValue> : IReadOnlySet<TValue>
    {
        private IReadOnlySet<TValue> _state;

        public DelegateReadOnlySet(IReadOnlySet<TValue> state)
        {
            _state = state;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _state.GetEnumerator();
        }

        public int Count => _state.Count;

        public bool Contains(TValue item)
        {
            return _state.Contains(item);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            _state.CopyTo(array, arrayIndex);
        }
    }
}