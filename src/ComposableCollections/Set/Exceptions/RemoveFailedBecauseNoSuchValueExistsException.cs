using System;

namespace ComposableCollections.Set.Exceptions
{
    public class RemoveFailedBecauseNoSuchValueExistsException : Exception
    {
        public object Value { get; }

        public RemoveFailedBecauseNoSuchValueExistsException(object value)
        {
            Value = value;
        }
    }
}