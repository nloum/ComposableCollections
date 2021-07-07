using System;
using Humanizer;
using Microsoft.CodeAnalysis;

namespace CodeIO.SourceCode.Read
{
    public static class Extensions
    {
        public static TypeIdentifier GetTypeIdentifier(this INamedTypeSymbol symbol)
        {
            return new TypeIdentifier()
            {
                Arity = symbol.Arity,
                Name = symbol.Name,
                Namespace = symbol.ContainingNamespace.GetFullNamespace()
            };
        }
        
        public static TypeReader AddSupportForSourceCode(this TypeReader typeReader)
        {
            return typeReader.AddTypeFormat<INamedTypeSymbol>(type => type.GetTypeIdentifier(), (symbol, lazyTypes) =>
            {
                throw new NotImplementedException($"No support for {symbol.TypeKind.ToString().Pluralize()} yet");
            });
        }

        public static TypeReader AddCompilation(this TypeReader typeReader, Compilation compilation)
        {
            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                var root = syntaxTree.GetRoot();
                typeReader.AddSyntaxNode(root, semanticModel);
            }

            return typeReader;
        }

        public static TypeReader AddSyntaxNode(this TypeReader typeReader, SyntaxNode syntaxNode, SemanticModel semanticModel)
        {
            var symbol = semanticModel.GetDeclaredSymbol(syntaxNode) as INamedTypeSymbol;
            if (symbol != null)
            {
                if (symbol is not IErrorTypeSymbol)
                {
                    var x = typeReader.GetTypeFormat<INamedTypeSymbol>()[symbol];
                }
            }
            
            foreach (var child in syntaxNode.ChildNodes())
            {
                typeReader.AddSyntaxNode(child, semanticModel);
            }

            return typeReader;
        }

        public static string GetFullNamespace(this INamespaceSymbol namespaceSymbol)
        {
            if (namespaceSymbol.IsGlobalNamespace)
            {
                return "";
            }
            
            var result = namespaceSymbol.ToString();
            return result;
        }
    }
}