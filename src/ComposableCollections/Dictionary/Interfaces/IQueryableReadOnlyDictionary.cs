using System.Linq;

namespace ComposableCollections.Dictionary
{
    public interface IQueryableReadOnlyDictionary<TKey, out TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        new IQueryable<TValue> Values { get; }
    }
}