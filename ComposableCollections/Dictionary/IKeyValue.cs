﻿namespace ComposableCollections.Dictionary
{
    /// <summary>
    /// Same as the normal .NET KeyValuePair class, except for this class the type parameters vary covariantly.
    /// </summary>
    public interface IKeyValue<out TKey, out TValue>
    {
        TKey Key { get; }
        TValue Value { get; }
    }
}
