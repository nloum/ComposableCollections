using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Interfaces
{
    /// <summary>
    /// A dictionary that will keep track of the changes you make to it, and then allows you to flush those changes
    /// to another dictionary.
    /// </summary>
    public interface ICachedWriteDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        void FlushCache();
    }
}