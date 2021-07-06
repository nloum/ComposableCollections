using System;

namespace ComposableCollections.Dictionary.Exceptions
{
    public class AddFailedBecauseKeyAlreadyExistsException : Exception
    {
        public object Key { get; }

        public AddFailedBecauseKeyAlreadyExistsException(object key)
        {
            Key = key;
        }
    }
}