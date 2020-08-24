using System;

namespace ComposableCollections.Common
{
    public interface ITransactionalCollection<out TReadOnly, out TReadWrite> : IReadOnlyTransactionalCollection<TReadOnly> where TReadOnly : IDisposable where TReadWrite : IDisposable
    {
        TReadWrite BeginWrite();
    }
}