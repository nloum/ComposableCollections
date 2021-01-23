﻿using System.Collections.Generic;

namespace DebuggableSourceGenerators
{
    public class SymbolBoundGenericClass : IBoundGenericStructuredType
    {
        public SymbolBoundGenericClass(TypeIdentifier identifier, IStructuredType unboundForm, IReadOnlyList<IType> typeParameterValues)
        {
            Identifier = identifier;
            UnboundForm = unboundForm;
            TypeParameterValues = typeParameterValues;
        }

        public TypeIdentifier Identifier { get; }
        public IStructuredType UnboundForm { get; }
        public IReadOnlyList<IType> TypeParameterValues { get; }
    }
}