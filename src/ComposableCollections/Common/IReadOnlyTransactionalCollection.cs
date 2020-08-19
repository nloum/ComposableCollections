using System;

namespace ComposableCollections.Dictionary
{
    public interface IReadOnlyTransactionalCollection<out TReadOnly> where TReadOnly : IDisposable
    {
        TReadOnly BeginRead();
    }
}