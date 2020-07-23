using System.Collections.Specialized;

namespace MoreCollections
{
    public interface IReadOnlyBindableDictionary<TKey, out TValue> : INotifyCollectionChanged, IReadOnlyDictionaryEx<TKey, TValue>
    {
    }
}