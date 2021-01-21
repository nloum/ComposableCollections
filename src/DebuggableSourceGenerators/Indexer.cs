using System;
using System.Collections.Generic;
using System.Linq;

namespace DebuggableSourceGenerators
{
    public class Indexer
    {
        public Indexer(Lazy<IType> returnType, IReadOnlyList<Parameter> parameters)
        {
            ReturnType = returnType;
            Parameters = parameters;
        }

        public Lazy<IType> ReturnType { get; }

        public IReadOnlyList<Parameter> Parameters { get; }

        public override string ToString()
        {
            return $"{ReturnType.Value} this[{string.Join(", ", Parameters.Select(p => p.ToString()))}];";
        }
    }
}