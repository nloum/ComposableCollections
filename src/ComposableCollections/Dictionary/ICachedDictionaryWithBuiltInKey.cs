using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    /// <summary>
    /// A dictionary with a built-in key that will keep track of the changes you make to it, and then allows you to flush
    /// those changes to another dictionary.
    /// </summary>
    public interface ICachedDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>
    {
        IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> AsBypassCache();
        IDictionaryWithBuiltInKey<TKey, TValue> AsNeverFlush();
        void FlushCache();
        IEnumerable<DictionaryMutation<TKey, TValue>> GetMutations(bool clear);
    }
}