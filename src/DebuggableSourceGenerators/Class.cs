﻿using System.Collections.Immutable;

namespace DebuggableSourceGenerators
{
    public record Class : StructuredType
    {
        public ImmutableList<Constructor> Constructors { get; init; }
        public ImmutableList<Field> Fields { get; init; }
    }
}