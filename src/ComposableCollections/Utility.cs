﻿using GenericNumbers;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComposableCollections.Dictionary;
using static GenericNumbers.NumbersUtility;

namespace ComposableCollections
{
    public static class Utility
    {
        public static IKeyValuePair<TKey, TValue> KeyValuePair<TKey, TValue>(TKey key, TValue value)
        {
            return new KeyValuePairImpl<TKey, TValue>(key, value);
        }
    }
}
