using System.Collections.Generic;

namespace ComposableDataStructures.Crdt
{
    public interface ICrdt<in TWrite>
    {
        void Write(IEnumerable<TWrite> writes);
    }
}