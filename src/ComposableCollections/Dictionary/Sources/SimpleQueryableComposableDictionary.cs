using System.Linq;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Sources
{
    public class SimpleQueryableComposableDictionary<TKey, TValue> : ComposableDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue>
    {
        public new IQueryable<TValue> Values => base.Values.AsQueryable();
    }
}