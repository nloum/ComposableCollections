using System;

namespace ComposableCollections.Common
{
    public interface IReadWriteFactory<out TReadOnly, out TReadWrite> : IReadOnlyFactory<TReadOnly>
    {
        TReadWrite CreateWriter();
    }
}