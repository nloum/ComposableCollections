using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    public interface ICachingDictionary<TKey, TValue> : IDictionaryEx<TKey, TValue>
    {
        IReadOnlyDictionaryEx<TKey, TValue> AsBypassCache();
        IDictionaryEx<TKey, TValue> AsNeverFlush();
        void FlushCache();
        IEnumerable<DictionaryMutation<TKey, TValue>> GetMutations(bool clear);
    }
}