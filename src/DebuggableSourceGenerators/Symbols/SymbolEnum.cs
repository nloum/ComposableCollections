using System.Collections.Generic;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators
{
    public class SymbolEnum : IEnum
    {
        public TypeIdentifier Identifier { get; }
        public IReadOnlyList<string> Values { get; }

        public SymbolEnum(TypeIdentifier identifier, IEnumerable<string> values)
        {
            Identifier = identifier;
            Values = values.ToImmutableList();
        }
    }
}