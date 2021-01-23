using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DebuggableSourceGenerators
{
    public class SyntaxClass : IClass
    {
        readonly ITypeRegistryService TypeRegistryService;
        readonly ISyntaxService SyntaxService;

        public SyntaxClass(ITypeRegistryService typeRegistryService, ISyntaxService syntaxService)
        {
            TypeRegistryService = typeRegistryService;
            SyntaxService = syntaxService;
        }
        
        public void Initialize(TypeIdentifier identifier, ClassDeclarationSyntax[] classDeclarationSyntaxes)
        {
            var firstClassDeclarationSyntax = classDeclarationSyntaxes[0];
            Identifier = identifier;

            TypeParameters = SyntaxService.Convert(firstClassDeclarationSyntax.TypeParameterList).ToImmutableList();

            var properties = new List<Property>();
            var indexers = new List<Indexer>();
            var methods = new List<Method>();
            
            foreach (var classDeclarationSyntax in classDeclarationSyntaxes)
            {
                SyntaxService.Convert(classDeclarationSyntax.Members, out var tmpProperties, out var tmpIndexers, out var tmpMethods);
                properties.AddRange(tmpProperties);
                indexers.AddRange(tmpIndexers);
                methods.AddRange(tmpMethods);
            }
            
            Properties = properties;
            Indexers = indexers;
            Methods = methods;
        }

        public TypeIdentifier Identifier { get; private set; }
        public IReadOnlyList<TypeParameter> TypeParameters { get; private set; }
        public IReadOnlyList<Property> Properties { get; private set; }
        public IReadOnlyList<Method> Methods { get; private set; }
        public IReadOnlyList<Indexer> Indexers { get; private set; }
    }
}