using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComposableCollections.CodeGenerator
{
    public class DecoratorBaseGenerator : IGenerator<DecoratorBaseGeneratorSettings>
    {
        private DecoratorBaseGeneratorSettings _settings;

        public void Initialize(DecoratorBaseGeneratorSettings settings)
        {
            _settings = settings;
        }

        public ImmutableDictionary<string, string> Generate(IEnumerable<SyntaxTree> syntaxTrees, Func<SyntaxTree, SemanticModel> getSemanticModel)
        {
            var interfaceDeclarations = new Dictionary<string, InterfaceDeclarationSyntax>();
            var syntaxTreeForEachInterface = new Dictionary<InterfaceDeclarationSyntax, SyntaxTree>();

            var syntaxTreesList = syntaxTrees.ToImmutableList();
            
            foreach (var syntaxTree in syntaxTreesList)
            {
                Utilities.TraverseTree(syntaxTree.GetRoot(), node =>
                {
                    if (node is InterfaceDeclarationSyntax interfaceDeclarationSyntax)
                    {
                        interfaceDeclarations.Add(interfaceDeclarationSyntax.Identifier.Text, interfaceDeclarationSyntax);
                        syntaxTreeForEachInterface[interfaceDeclarationSyntax] = syntaxTree;
                    }
                });
            }

            var results = new Dictionary<string, string>();

            foreach (var iface in _settings.InterfacesToImplement)
            {
                var interfaceDeclaration = interfaceDeclarations[iface];
                
                var semanticModel = getSemanticModel(syntaxTreeForEachInterface[interfaceDeclaration]);

                var duplicateMembers = new DuplicateMembersService(interfaceDeclaration, semanticModel);
                var delegateMember = new DelegateMemberService();
                
                var className = $"{iface.Substring(1)}DecoratorBase";
                var sourceCodeBuilder = new StringBuilder();

                var typeParameters = interfaceDeclaration.TypeParameterList.Parameters.Select(tps => tps.Identifier.Text).ToImmutableList();
                var genericParams = "";
                if (typeParameters.Count > 0)
                {
                    genericParams = $"<{string.Join(", ", typeParameters)}>";
                }

                var usings = new List<string>();

                sourceCodeBuilder.AppendLine(
                    $"namespace {_settings.Namespace} {{\npublic class {className}{genericParams} : {iface}{genericParams} {{");

                //var parameterName = Utilities.GenerateVariableName(interfaceDeclaration.Identifier.Text, true);
                var parameterName = "decoratedObject";
                
                var parameters = $"{interfaceDeclaration.Identifier}{genericParams} {parameterName}";

                var parameterString = string.Join(", ", parameters);
                //var fieldName = "_" + parameterName;
                var fieldName = "_decoratedObject";

                sourceCodeBuilder.AppendLine($"private readonly {interfaceDeclaration.Identifier}{genericParams} {fieldName};");

                sourceCodeBuilder.AppendLine($"public {className}({parameterString}) {{");
                
                sourceCodeBuilder.AppendLine($"{fieldName} = {parameterName};");
                sourceCodeBuilder.AppendLine("}");
                
                var members = Utilities.GetMembersGroupedByDeclaringType(interfaceDeclaration, semanticModel);
                
                foreach (var member in members)
                {
                    foreach (var memberItem in member.Value)
                    {
                        delegateMember.DelegateMember(memberItem, fieldName, member.Key, duplicateMembers.IsDuplicate(memberItem), sourceCodeBuilder, usings);
                    }
                }

                sourceCodeBuilder.AppendLine("}\n}\n");

                usings = usings.Distinct().OrderBy(x => x).ToList();
                
                results.Add($"{className}.cs", $"{string.Join("\n",usings)}\n{sourceCodeBuilder}");
            }

            return results.ToImmutableDictionary();
        }

        
    }
}