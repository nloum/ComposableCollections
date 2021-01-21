using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DebuggableSourceGenerators
{
    public class SyntaxService : ISyntaxService
    {
        readonly ITypeRegistryService TypeRegistryService;
        readonly Func<SyntaxTree, SemanticModel> GetSemanticModel;
        ISymbolService SymbolService;

        public SyntaxService(ITypeRegistryService typeRegistryService, ISymbolService symbolService, Func<SyntaxTree, SemanticModel> getSemanticModel)
        {
            TypeRegistryService = typeRegistryService;
            SymbolService = symbolService;
            GetSemanticModel = getSemanticModel;
        }

        public IEnumerable<Parameter> Convert(SeparatedSyntaxList<ParameterSyntax> parameterListSyntax)
        {
            foreach (var item in parameterListSyntax)
            {
                var type = Convert(item.Type);
                yield return new Parameter(item.Identifier.Text, type, ParameterMode.In);
            }
        }

        public IEnumerable<TypeParameter> Convert(TypeParameterListSyntax typeParameterListSyntax)
        {
            if (typeParameterListSyntax == null)
            {
                yield break;
            }
            
            foreach (var item in typeParameterListSyntax.Parameters)
            {
                var varianceMode = VarianceMode.None;

                if (item.VarianceKeyword.Text == "in")
                {
                    varianceMode = VarianceMode.In;
                }
                if (item.VarianceKeyword.Text == "out")
                {
                    varianceMode = VarianceMode.Out;
                }
                
                yield return new TypeParameter(item.Identifier.Text, varianceMode);
            }
        }

        public Lazy<IType> Convert(TypeSyntax typeSyntax)
        {
            var symbol = GetSemanticModel(typeSyntax.SyntaxTree).GetSymbolInfo(typeSyntax);
            var namedTypeSymbol = symbol.Symbol as INamedTypeSymbol;

            if (namedTypeSymbol == null)
            {
                return new Lazy<IType>(new TypeParameter(typeSyntax.ToString(), VarianceMode.None));
            }

            return SymbolService.GetType(namedTypeSymbol);
        }

        public void Convert(SyntaxList<MemberDeclarationSyntax> memberDeclarationSyntaxes, out IReadOnlyList<Property> properties, out IReadOnlyList<Indexer> indexers, out IReadOnlyList<Method> methods)
        {
            var tmpProperties = new List<Property>();
            var tmpIndexers = new List<Indexer>();
            var tmpMethods = new List<Method>();
            
            foreach (var member in memberDeclarationSyntaxes)
            {
                if (member is PropertyDeclarationSyntax propertyDeclarationSyntax)
                {
                    tmpProperties.Add(new Property(propertyDeclarationSyntax.Identifier.Text, Convert(propertyDeclarationSyntax.Type)));
                }
                else if (member is IndexerDeclarationSyntax indexerDeclarationSyntax)
                {
                    tmpIndexers.Add(new Indexer(TypeRegistryService.GetType(indexerDeclarationSyntax.Type.ToString()), Convert(indexerDeclarationSyntax.ParameterList.Parameters).ToImmutableList()));
                }
                else if (member is MethodDeclarationSyntax methodDeclarationSyntax)
                {
                    tmpMethods.Add(new Method(Convert(methodDeclarationSyntax.ReturnType), methodDeclarationSyntax.Identifier.Text, Convert(methodDeclarationSyntax.ParameterList.Parameters).ToImmutableList()));
                }
            }

            properties = tmpProperties;
            indexers = tmpIndexers;
            methods = tmpMethods;
        }
    }
}