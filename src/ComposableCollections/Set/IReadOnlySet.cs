using System.Collections.Generic;

namespace ComposableCollections.Set
{
    public interface IReadOnlySet<out TValue> : IReadOnlyCollection<TValue>
    {
    }
}