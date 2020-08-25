using System;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface
        IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
            IDisposable
    {
        IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary();
    }
}