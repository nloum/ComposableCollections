using System.Collections.Immutable;

namespace DebuggableSourceGenerators.Read
{
    public record Enum : Type
    {
        public ImmutableList<string> Values { get; init; }
    }
}