using System;
using System.Collections.Generic;

namespace SimpleMonads.HotChocolate
{
    internal class EitherMetadata
    {
        public Type ConvertibleTo { get; init; }
        public Type SubTypesOf { get; init; }
        public IReadOnlyList<Type> Types { get; init; }
    }
}