using System;

namespace ComposableCollections.Common
{
    public interface IDisposableReadOnlyFactory<out TReadOnly> : IReadOnlyFactory<TReadOnly>, IDisposable 
    {
    }
}