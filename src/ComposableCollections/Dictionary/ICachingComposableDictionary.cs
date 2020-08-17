using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    public interface ICachingComposableDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        IReadOnlyDictionaryEx<TKey, TValue> AsBypassCache();
        IComposableDictionary<TKey, TValue> AsNeverFlush();
        void FlushCache();
        IEnumerable<DictionaryMutation<TKey, TValue>> GetMutations(bool clear);
    }
}