using System;

namespace ComposableCollections.Dictionary
{
    public interface IDisposableTransactionalCollection<out TReadOnly, out TReadWrite> : ITransactionalCollection<TReadOnly, TReadWrite>, IDisposable where TReadOnly : IDisposable where TReadWrite : IDisposable
    {
    }
}