using System;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators.Read
{
    public record BoundClass : BoundStructuredType
    {
        public ImmutableList<Constructor> Constructors { get; init; }
        public ImmutableList<Field> Fields { get; init; }
        public Lazy<BoundClass> BaseClass { get; init; }
    }
}