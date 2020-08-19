using System;

namespace ComposableCollections.Dictionary
{
    public interface IDisposableReadOnlyDictionary<TKey, out TValue> : IComposableReadOnlyDictionary<TKey, TValue>,
        IDisposable
    {
    }
}