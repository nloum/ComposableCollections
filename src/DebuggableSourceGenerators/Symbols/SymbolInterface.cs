using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace DebuggableSourceGenerators
{
    public class SymbolInterface : IInterface
    {
        readonly ITypeRegistryService TypeRegistryService;
        readonly ISymbolService SymbolService;

        public SymbolInterface(ITypeRegistryService typeRegistryService, ISymbolService symbolService)
        {
            TypeRegistryService = typeRegistryService;
            SymbolService = symbolService;
        }

        public void Initialize(INamedTypeSymbol namedTypeSymbol)
        {
            Name = namedTypeSymbol.Name;

            var properties = new List<Property>();
            var indexers = new List<Indexer>();
            var methods = new List<Method>();
            
            SymbolService.ConvertMembers(namedTypeSymbol, out var tmpProperties, out var tmpIndexers, out var tmpMethods);
            properties.AddRange(tmpProperties);
            indexers.AddRange(tmpIndexers);
            methods.AddRange(tmpMethods);
            
            Properties = properties;
            Indexers = indexers;
            Methods = methods;
        }
        
        public string Name { get; private set; }
        public IReadOnlyList<TypeParameter> TypeParameters { get; private set; }
        public IReadOnlyList<Property> Properties { get; private set; }
        public IReadOnlyList<Method> Methods { get; private set; }
        public IReadOnlyList<Indexer> Indexers { get; private set; }
    }
}