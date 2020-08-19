using System.Linq;

namespace ComposableCollections.Dictionary
{
    public interface IQueryableDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        new IQueryable<TValue> Values { get; }
    }
}