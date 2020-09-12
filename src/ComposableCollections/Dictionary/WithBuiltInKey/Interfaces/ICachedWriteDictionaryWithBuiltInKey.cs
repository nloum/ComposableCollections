using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    /// <summary>
    /// A dictionary with a built-in key that will keep track of the changes you make to it, and then allows you to flush
    /// those changes to another dictionary.
    /// </summary>
    public interface ICachedWriteDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>
    {
        IWriteCachedDictionary<TKey, TValue> AsCachedDictionary();
        void FlushCache();
    }
}