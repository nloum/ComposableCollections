using System.Collections.Specialized;

namespace ComposableCollections.Dictionary
{
    public interface IReadOnlyBindableDictionary<TKey, out TValue> : INotifyCollectionChanged, IComposableReadOnlyDictionary<TKey, TValue>
    {
    }
}