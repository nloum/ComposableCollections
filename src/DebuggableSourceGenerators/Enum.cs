using System.Collections.Generic;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators
{
    public record Enum : Type
    {
        public ImmutableList<string> Values { get; init; }
    }
}