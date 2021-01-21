using System.Collections.Generic;

namespace DebuggableSourceGenerators
{
    public class SymbolBoundGenericInterface : IBoundGenericStructuredType
    {
        public SymbolBoundGenericInterface(string name, IStructuredType unboundForm, IReadOnlyList<IType> typeParameterValues)
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