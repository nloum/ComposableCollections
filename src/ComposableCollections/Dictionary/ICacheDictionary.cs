using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    public interface ICacheDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        IComposableReadOnlyDictionary<TKey, TValue> AsBypassCache();
        IComposableDictionary<TKey, TValue> AsNeverFlush();
        void FlushCache();
        IEnumerable<DictionaryMutation<TKey, TValue>> GetMutations(bool clear);
    }
}