using System;

namespace ComposableCollections.Common
{
    public interface IReadOnlyFactory<out TReadOnly> where TReadOnly : IDisposable
    {
        TReadOnly CreateReader();
    }
}