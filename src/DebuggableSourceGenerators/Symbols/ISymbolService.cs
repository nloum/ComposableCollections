using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace DebuggableSourceGenerators
{
    public interface ISymbolService
    {
        string Convert(INamespaceSymbol namespaceSymbol);
        Lazy<IType> GetType(ITypeSymbol symbol);
        Lazy<IType> GetType(INamedTypeSymbol symbol);
        void ConvertMembers(INamedTypeSymbol symbol, out IReadOnlyList<Property> properties, out IReadOnlyList<Indexer> indexers,
            out IReadOnlyList<Method> methods);

        void LoadTypesFromAssemblies();
    }
}