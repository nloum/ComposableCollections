using System.Collections.Generic;

namespace ComposableCollections.Set
{
    public interface IReadOnlySet<TValue> : IReadOnlyCollection<TValue>
    {
        bool Contains(TValue item);
        void CopyTo(TValue[] array, int arrayIndex);
    }
}