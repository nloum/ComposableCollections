using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComposableCollections.CodeGenerator
{
    public class ExtensionMethodVariationGenerator : IGenerator<ExtensionMethodVariationGeneratorSettings>
    {
        private ExtensionMethodVariationGeneratorSettings _settings;

        public void Initialize(ExtensionMethodVariationGeneratorSettings settings)
        {
            _settings = settings;
        }

        public ImmutableDictionary<string, string> Generate(IEnumerable<SyntaxTree> syntaxTrees, Func<SyntaxTree, SemanticModel> getSemanticModel)
        {
            foreach (var syntaxTree in syntaxTrees)
            {
                var methodDeclarations = Utilities.GetDescendantsOfType<MethodDeclarationSyntax>(syntaxTree.GetRoot());
                var extensionMethods = methodDeclarations.Where(methodDeclaration =>
                    methodDeclaration.Identifier.Text == _settings.ExtensionMethodName && 
                    methodDeclaration.ParameterList.Parameters.Count > 0
                    && methodDeclaration.ParameterList.Parameters.First().Modifiers
                        .Any(modifier =>
                            modifier.IsKind(SyntaxKind.ThisKeyword)))
                    .ToImmutableList();
            }
            
            return ImmutableDictionary<string, string>.Empty;
        }
    }
}