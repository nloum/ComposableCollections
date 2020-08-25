using System.Linq;

namespace ComposableCollections.Dictionary.Interfaces
{
    public interface IQueryableReadOnlyDictionary<TKey, out TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        new IQueryable<TValue> Values { get; }
    }
}