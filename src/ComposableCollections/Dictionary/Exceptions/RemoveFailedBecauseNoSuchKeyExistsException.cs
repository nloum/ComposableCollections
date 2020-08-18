using System;

namespace ComposableCollections.Dictionary.Exceptions
{
    public class RemoveFailedBecauseNoSuchKeyExistsException : Exception
    {
        public object Key { get; }

        public RemoveFailedBecauseNoSuchKeyExistsException(object key)
        {
            Key = key;
        }
    }
}