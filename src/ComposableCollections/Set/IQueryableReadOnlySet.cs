using System.Linq;

namespace ComposableCollections.Set
{
    public interface IQueryableReadOnlySet<TValue> : IReadOnlySet<TValue>, IQueryable<TValue>
    {
    }
}