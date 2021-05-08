using System;
using System.Collections.Generic;
using System.Linq;

namespace DebuggableSourceGenerators.Read
{
    public record Method
    {
        public bool IsStatic { get; init; }
        public Lazy<Type> ReturnType { get; init; }
        public string Name { get; init; }
        public IReadOnlyList<Parameter> Parameters { get; init; }
        public Visibility Visibility { get; init; }
        public bool IsVirtual { get; init; }
        public IReadOnlyList<string> TypeParameters { get; init; }

        public override string ToString()
        {
            return $"{Visibility} {ReturnType.Value} {Name}({string.Join(", ", Parameters.Select(x => x.ToString()))});";
        }
    }
}