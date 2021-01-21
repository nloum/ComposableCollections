using System;
using System.Collections.Generic;
using System.Linq;

namespace DebuggableSourceGenerators
{
    public class Method
    {
        public Method(Lazy<IType> returnType, string name, IReadOnlyList<Parameter> parameters)
        {
            ReturnType = returnType;
            Name = name;
            Parameters = parameters;
        }

        public Lazy<IType> ReturnType { get; }
        
        public string Name { get; }
        public IReadOnlyList<Parameter> Parameters { get; }

        public override string ToString()
        {
            return $"{ReturnType.Value} {Name}({string.Join(", ", Parameters.Select(x => x.ToString()))});";
        }
    }
}