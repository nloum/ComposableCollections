using System;

namespace ComposableCollections.Set
{
    public interface IDisposableReadOnlySet<TValue> : IReadOnlySet<TValue>, IDisposable
    {
        
    }
}