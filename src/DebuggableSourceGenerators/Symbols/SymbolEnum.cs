using System.Collections.Generic;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators
{
    public class SymbolEnum : IEnum
    {
        public string Name { get; }
        public IReadOnlyList<string> Values { get; }

        public SymbolEnum(string name, IEnumerable<string> values)
        {
            Name = name;
            Values = values.ToImmutableList();
        }
    }
}