using System;

namespace ComposableCollections.Dictionary.Interfaces
{
    public interface IDisposableReadOnlyDictionary<TKey, out TValue> : IComposableReadOnlyDictionary<TKey, TValue>, IDisposable { }
}