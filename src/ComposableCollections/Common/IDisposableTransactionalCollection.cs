using System;

namespace ComposableCollections.Common
{
    public interface IDisposableTransactionalCollection<out TReadOnly, out TReadWrite> : ITransactionalCollection<TReadOnly, TReadWrite>, IDisposable where TReadOnly : IDisposable where TReadWrite : IDisposable
    {
    }
}