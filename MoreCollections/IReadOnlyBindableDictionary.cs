using System.Collections.Generic;
using System.Collections.Specialized;

namespace MoreCollections
{
    public interface IReadOnlyBindableDictionary<TKey, TValue> : INotifyCollectionChanged,
        IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<IKeyValuePair<TKey, TValue>>
    {
    }
}