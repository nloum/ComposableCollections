using System;

namespace ComposableCollections.Common
{
    public interface IReadWriteFactory<out TReadOnly, out TReadWrite> : IReadOnlyFactory<TReadOnly> where TReadOnly : IDisposable where TReadWrite : IDisposable
    {
        TReadWrite CreateWriter();
    }
}