using System.Linq;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        new IQueryable<TValue> Values { get; }
        IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary();
    }
}