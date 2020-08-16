using System.Collections.Specialized;

namespace ComposableCollections.Dictionary
{
    public interface IReadOnlyBindableDictionary<TKey, out TValue> : INotifyCollectionChanged, IReadOnlyDictionaryEx<TKey, TValue>
    {
    }
}