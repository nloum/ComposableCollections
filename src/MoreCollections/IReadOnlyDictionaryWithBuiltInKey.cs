using System.Collections.Generic;
using SimpleMonads;

namespace MoreCollections
{
    public interface IReadOnlyDictionaryWithBuiltInKey<TKey, out TValue> : IReadOnlyDictionaryEx<TKey, TValue>
    {
    }
}