using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using CodeIO.LoadedTypes.Read;
using CodeIO.UnloadedAssemblies.Read;
using Humanizer;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Mono.Cecil;
using Mono.Cecil.Rocks;
using NuGet.Common;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace CodeIO.CSharp.Read
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