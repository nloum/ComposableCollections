using System;

namespace ComposableCollections.Set.Exceptions
{
    public class AddFailedBecauseValueAlreadyExistsException : Exception
    {
        public object Value { get; }

        public AddFailedBecauseValueAlreadyExistsException(object value)
        {
            Value = value;
        }
    }
}