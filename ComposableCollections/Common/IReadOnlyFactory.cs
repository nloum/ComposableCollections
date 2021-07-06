using System;

namespace ComposableCollections.Common
{
    public interface IReadOnlyFactory<out TReadOnly>
    {
        TReadOnly CreateReader();
    }
}