using System;

namespace ComposableCollections.Common
{
    public static class ReadOnlyFactory
    {
        public static IReadOnlyFactory<TReadOnly> Create<TReadOnly>(Func<TReadOnly> readOnly)
            
        {
            return new AnonymousReadOnlyFactory<TReadOnly>(readOnly);
        }
    }
}