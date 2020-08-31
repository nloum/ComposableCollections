using System.Collections.Generic;
using ComposableCollections.Set.Write;

namespace ComposableCollections.Set
{
    public interface ICachedSet<TValue> : IComposableSet<TValue>
    {
        IReadOnlySet<TValue> AsBypassCache();
        IComposableSet<TValue> AsNeverFlush();
        void FlushCache();
        IEnumerable<SetWrite<TValue>> GetWrites(bool clear);
    }
}