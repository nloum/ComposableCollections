using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MoreCollections;

namespace ComposableCollections.CodeGenerator
{
    public class AnonymousImplementationGenerator : IGenerator<AnonymousImplementationGeneratorSettings>
    {
        private AnonymousImplementationGeneratorSettings _settings;

        public void Initialize(AnonymousImplementationGeneratorSettings settings)
        {
            _settings = settings;
        }

        private void TraverseTree(SyntaxNode syntaxNode, Action<SyntaxNode> visit)
        {
            visit(syntaxNode);
            foreach (var child in syntaxNode.ChildNodes())
            {
                TraverseTree(child, visit);
            }
        }

        public ImmutableDictionary<string, string> Generate(IEnumerable<SyntaxTree> syntaxTrees, Func<SyntaxTree, SemanticModel> getSemanticModel)
        {
            var interfaceDeclarations = new Dictionary<string, InterfaceDeclarationSyntax>();
            var syntaxTreeForEachInterface = new Dictionary<InterfaceDeclarationSyntax, SyntaxTree>();

            var syntaxTreesList = syntaxTrees.ToImmutableList();
            
            foreach (var syntaxTree in syntaxTreesList)
            {
                TraverseTree(syntaxTree.GetRoot(), node =>
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
                
                var className = $"Anonymous{iface.Substring(1)}";
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

                var semanticModel = getSemanticModel(syntaxTreeForEachInterface[interfaceDeclaration]);
                var interfacesToImplement = GetInterfacesToImplementWith(interfaceDeclaration, semanticModel);

                foreach (var interfaceToImplement in interfacesToImplement)
                {
                    var baseInterfaces = Utilities.GetBaseInterfaces(interfaceToImplement);
                    foreach (var item in baseInterfaces.SelectMany(baseInterface => baseInterface.DeclaringSyntaxReferences))
                    {
                        var moreUsings = Utilities.GetDescendantsOfType<UsingDirectiveSyntax>(item.SyntaxTree.GetRoot())
                            .Select(us => $"using {us.Name};");
                        usings.AddRange(moreUsings);
                    }
                    usings.Add($"using {string.Join(".", interfaceToImplement.ContainingNamespace.ConstituentNamespaces)};");
                }

                var parameterTypesInOrder = interfacesToImplement.Select(interfaceToImplement =>
                {
                    if (interfaceToImplement.TypeParameters.Length == 0)
                    {
                        return interfaceToImplement.Name;
                    }
                    
                    return $"{interfaceToImplement.Name}<{string.Join(", ", interfaceToImplement.TypeParameters)}>";
                }).ToImmutableList();

                var parameterNamesInOrder = interfacesToImplement.Select(baseType => baseType.Name)
                    .Select(baseType => Utilities.GenerateVariableName(baseType, true)).ToImmutableList();
                
                var parameters = parameterTypesInOrder.Zip(parameterNamesInOrder).Select((x) =>
                    $"{x.First} {x.Second}");

                var parameterString = string.Join(", ", parameters);

                var fieldDefinitions =
                    parameterTypesInOrder.Zip(parameterNamesInOrder).Select((type) => $"private {type.First} _{type.Second};");
                sourceCodeBuilder.AppendLine(string.Join("\n", fieldDefinitions));
                
                sourceCodeBuilder.AppendLine($"public {className}({parameterString}) {{");
                
                var fieldAssignments = parameterNamesInOrder
                    .Select(name => $"_{name} = {name};");
                sourceCodeBuilder.AppendLine(string.Join("\n", fieldAssignments));
                sourceCodeBuilder.AppendLine("}");
                
                var delegateMemberService = new DelegateMemberService();

                var membersDelegated = new HashSet<ISymbol>();
                
                foreach (var interfaceToImplement in interfacesToImplement)
                {
                    var fieldName = "_" + Utilities.GenerateVariableName(interfaceToImplement.Name, true);
                    
                    var membersGroupedByDeclaringType = Utilities.GetMembersGroupedByDeclaringType(interfaceToImplement);
                    foreach (var groupOfMembers in membersGroupedByDeclaringType)
                    {
                        foreach (var member in groupOfMembers.Value)
                        {
                            if (!membersDelegated.Contains(member))
                            {
                                membersDelegated.Add(member);
                                delegateMemberService.DelegateMember(member, fieldName, groupOfMembers.Key, true, sourceCodeBuilder, usings);
                            }
                        }
                    }
                }
                
                sourceCodeBuilder.AppendLine("}\n}\n");

                usings = usings.Distinct().OrderBy(x => x).ToList();
                
                results.Add($"{className}.cs", $"{string.Join("\n",usings)}\n{sourceCodeBuilder}");
            }

            return results.ToImmutableDictionary();
        }

        private ImmutableList<INamedTypeSymbol> GetInterfacesToImplementWith(InterfaceDeclarationSyntax interfaceDeclaration,
            SemanticModel semanticModel)
        {
            var interfacesToImplement = interfaceDeclaration.BaseList.Types.SelectMany(baseType =>
            {
                var symbolInfo = semanticModel.GetSymbolInfo(baseType.Type);
                if (symbolInfo.Symbol is INamedTypeSymbol namedTypeSymbol)
                {
                    var result = new List<INamedTypeSymbol>();
                    GetInterfacesToImplementWith(namedTypeSymbol, result);
                    return result;
                }

                return Enumerable.Empty<INamedTypeSymbol>();
            }).Distinct().ToImmutableList();

            var interfacesToNotImplement = new List<INamedTypeSymbol>();

            for (var i = 0; i < interfacesToImplement.Count; i++)
            {
                for (var j = 0; j < interfacesToImplement.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (Utilities.IsBaseInterface(interfacesToImplement[i], interfacesToImplement[j]))
                    {
                        interfacesToNotImplement.Add(interfacesToImplement[j]);
                    }
                }
            }

            interfacesToImplement = interfacesToImplement.Except(interfacesToNotImplement).ToImmutableList();
            return interfacesToImplement;
        }

        private void GetInterfacesToImplementWith(INamedTypeSymbol superInterface, List<INamedTypeSymbol> result)
        {
            if (superInterface.MemberNames.Any())
            {
                result.Add(superInterface);
            }
            else
            {
                foreach (var baseType in superInterface.Interfaces)
                {
                    GetInterfacesToImplementWith(baseType, result);
                }
            }
        }
    }
}