using System;

namespace ComposableCollections.Common
{
    public interface IDisposableReadWriteFactory<out TReadOnly, out TReadWrite> : IReadWriteFactory<TReadOnly, TReadWrite>, IDisposable  
    {
    }
}