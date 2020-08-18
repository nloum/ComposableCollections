using System;

namespace ComposableCollections.Dictionary.Exceptions
{
    public class UpdateFailedBecauseNoSuchKeyExistsException : Exception
    {
        public object Key { get; }

        public UpdateFailedBecauseNoSuchKeyExistsException(object key)
        {
            Key = key;
        }
    }
}