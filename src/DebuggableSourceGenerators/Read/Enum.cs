using System.Collections.Immutable;

namespace DebuggableSourceGenerators.Read
{
    public record Enum : TypeBase
    {
        public ImmutableList<string> Values { get; init; }
    }
}