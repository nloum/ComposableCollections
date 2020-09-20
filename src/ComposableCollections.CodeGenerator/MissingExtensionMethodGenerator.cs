using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComposableCollections.CodeGenerator
{
    public class MissingExtensionMethodGenerator : IGenerator<MissingExtensionMethodGeneratorSettings>
    {
        private MissingExtensionMethodGeneratorSettings _settings;

        public void Initialize(MissingExtensionMethodGeneratorSettings settings)
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

    public class MissingExtensionMethodGeneratorSettings
    {
        [XmlAttribute("ExtensionMethodName")]
        public string ExtensionMethodName { get; set; }
    }
}