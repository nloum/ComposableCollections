using System;

namespace ComposableCollections.Set
{
    public interface IDisposableReadOnlySet<out TValue> : IReadOnlySet<TValue>, IDisposable
    {
        
    }
}