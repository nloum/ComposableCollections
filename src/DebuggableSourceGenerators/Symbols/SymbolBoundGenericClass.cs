using System.Collections.Generic;

namespace DebuggableSourceGenerators
{
    public class SymbolBoundGenericClass : IBoundGenericStructuredType
    {
        public SymbolBoundGenericClass(string name, IStructuredType unboundForm, IReadOnlyList<IType> typeParameterValues)
        {
            Name = name;
            UnboundForm = unboundForm;
            TypeParameterValues = typeParameterValues;
        }

        public string Name { get; }
        public IStructuredType UnboundForm { get; }
        public IReadOnlyList<IType> TypeParameterValues { get; }
    }
}