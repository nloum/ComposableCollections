using System;

namespace ComposableCollections.Set.Exceptions
{
    public class UpdateFailedBecauseNoSuchValueExistsException : Exception
    {
        public object Value { get; }

        public UpdateFailedBecauseNoSuchValueExistsException(object value)
        {
            Value = value;
        }
    }
}