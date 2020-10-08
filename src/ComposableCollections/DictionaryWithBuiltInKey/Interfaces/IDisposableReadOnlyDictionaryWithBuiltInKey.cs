using System;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface
        IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
            IDisposable
    {
        IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary();
    }
}