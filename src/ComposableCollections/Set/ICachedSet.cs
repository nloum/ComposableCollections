using System.Collections.Generic;
using ComposableCollections.Set.Write;

namespace ComposableCollections.Set
{
    public interface ICachedSet<TValue> : ISet<TValue>
    {
        IReadOnlySet<TValue> AsBypassCache();
        ISet<TValue> AsNeverFlush();
        void FlushCache();
        IEnumerable<SetWrite<TValue>> GetWrites(bool clear);
    }
}