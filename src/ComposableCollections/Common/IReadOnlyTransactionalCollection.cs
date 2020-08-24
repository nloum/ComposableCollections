using System;

namespace ComposableCollections.Common
{
    public interface IReadOnlyTransactionalCollection<out TReadOnly> where TReadOnly : IDisposable
    {
        TReadOnly BeginRead();
    }
}