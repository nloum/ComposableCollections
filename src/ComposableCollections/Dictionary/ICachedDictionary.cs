using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    /// <summary>
    /// A dictionary that will keep track of the changes you make to it, and then allows you to flush those changes
    /// to another dictionary.
    /// </summary>
    public interface ICachedDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        IComposableReadOnlyDictionary<TKey, TValue> AsBypassCache();
        IComposableDictionary<TKey, TValue> AsNeverFlush();
        void FlushCache();
        IEnumerable<DictionaryMutation<TKey, TValue>> GetMutations(bool clear);
    }
}