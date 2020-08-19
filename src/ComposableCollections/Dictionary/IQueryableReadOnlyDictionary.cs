using System.Linq;

namespace ComposableCollections.Dictionary
{
    public interface IQueryableReadOnlyDictionary<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        new IQueryable<TValue> Values { get; }
    }
}