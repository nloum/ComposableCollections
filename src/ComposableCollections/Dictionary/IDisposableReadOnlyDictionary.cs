using System;

namespace ComposableCollections.Dictionary
{
    public interface IDisposableReadOnlyDictionary<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>,
        IDisposable
    {
    }
}