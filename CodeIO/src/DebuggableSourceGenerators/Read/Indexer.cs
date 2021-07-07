using System;
using System.Collections.Generic;
using System.Linq;

namespace DebuggableSourceGenerators.Read
{
    public class Indexer
    {
        public bool IsStatic { get; init; }
        public Lazy<TypeBase> ReturnType { get; init; }

        public IReadOnlyList<Parameter> Parameters { get; init; }
        public Visibility GetterVisibility { get; init; }
        public Visibility SetterVisibility { get; init; }
        public bool IsVirtual { get; init; }

        public override string ToString()
        {
            return $"{ReturnType.Value} this[{string.Join(", ", Parameters.Select(p => p.ToString()))}];";
        }
    }
}