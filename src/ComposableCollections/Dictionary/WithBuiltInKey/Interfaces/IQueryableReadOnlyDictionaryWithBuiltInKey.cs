using System.Linq;

namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public interface IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        new IQueryable<TValue> Values { get; }
        IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary();
    }
}