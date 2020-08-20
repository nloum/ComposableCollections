using System;

namespace ComposableCollections.Dictionary
{
    public interface IDisposableReadOnlyTransactionalCollection<out TReadOnly> : IReadOnlyTransactionalCollection<TReadOnly>, IDisposable where TReadOnly : IDisposable
    {
    }
}