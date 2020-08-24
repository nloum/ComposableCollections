using System;

namespace ComposableCollections.Common
{
    public interface IDisposableReadOnlyTransactionalCollection<out TReadOnly> : IReadOnlyTransactionalCollection<TReadOnly>, IDisposable where TReadOnly : IDisposable
    {
    }
}