using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FluentSourceGenerators
{
    [Generator]
    public class FluentSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif 
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var syntaxRoots = context.Compilation.SyntaxTrees.SelectMany(syntaxTree =>
                    Utilities.GetDescendantsOfType<InterfaceDeclarationSyntax>(syntaxTree.GetRoot()));
            var ifacesToGenerateImplementationsFor = syntaxRoots
                .Where(iface =>
                {
                    foreach (var attr in iface.AttributeLists.SelectMany(list => list.Attributes))
                    {
                        if (attr.Name.ToString() == "AnonymousImplementation")
                        {
                            return true;
                        }
                    }

                    return false;
                })
                .ToImmutableList();
            
            foreach (var iface in _settings.InterfacesToImplement)
            { 
                var interfaceDeclaration = codeIndexerService.GetInterfaceDeclaration(iface);
                
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

                var semanticModel = codeIndexerService.GetSemanticModel(interfaceDeclaration.SyntaxTree);
                var delegateMemberCandidates = new List<MemberToBeDelegated>();
                var candidateParameters = new List<Parameter>();
                GetInterfacesToImplementWith(semanticModel.GetDeclaredSymbol(interfaceDeclaration), candidateParameters, delegateMemberCandidates);
                
                var delegateMemberService = new DelegateMemberService();
                var memberDeduplicationService = new MemberDeduplicationService();
                var deduplicatedDelegatedMembers = memberDeduplicationService
                    .DeduplicateMembers(delegateMemberCandidates, delegated => delegated.Member).ToImmutableList();
                var parameters = candidateParameters.Where(candidateParameter =>
                        deduplicatedDelegatedMembers.Any(deduplicatedDelegatedMember =>
                            deduplicatedDelegatedMember.Value.MemberName == candidateParameter.MemberName))
                    .ToImmutableList();
                var getEnumerator = deduplicatedDelegatedMembers.FirstOrDefault(delegateMemberCandidate =>
                    delegateMemberCandidate.Value.Member.Name == "GetEnumerator");

                foreach (var baseInterface in Utilities.GetBaseInterfaces(semanticModel.GetDeclaredSymbol(interfaceDeclaration)))
                {
                    var newUsing =
                        $"using {string.Join(".", baseInterface.ContainingNamespace.ConstituentNamespaces)};";
                    if (!newUsing.Contains("global namespace"))
                    {
                        usings.Add(newUsing);
                    }
                }
                
                foreach (var usingDirective in Utilities.GetBaseInterfaces(semanticModel.GetDeclaredSymbol(interfaceDeclaration))
                    .SelectMany(symbol => symbol.DeclaringSyntaxReferences).Select(syntaxRef => syntaxRef.SyntaxTree)
                    .SelectMany(syntaxTree => Utilities.GetDescendantsOfType<UsingDirectiveSyntax>(syntaxTree.GetRoot())))
                {
                    var newUsing = usingDirective.ToString();
                    if (!newUsing.Contains("global namespace"))
                    {
                        usings.Add(newUsing);
                    }
                }

                var parameterString = string.Join(", ", parameters
                    .OrderBy(parameter => parameter.DelegateType).ThenBy(parameter => parameter.ParameterName)
                    .Select(parameter => $"{parameter.Type} {parameter.ParameterName}"));

                sourceCodeBuilder.AppendLine(string.Join("\n", parameters
                    .OrderBy(parameter => parameter.DelegateType).ThenBy(parameter => parameter.ParameterName)
                    .Select(parameter => parameter.ToMemberDeclaration())));
                
                sourceCodeBuilder.AppendLine($"public {className}({parameterString}) {{");
                
                var memberAssignments = parameters
                    .Select(parameter => $"{parameter.MemberName} = {parameter.ParameterName};");
                sourceCodeBuilder.AppendLine(string.Join("\n", memberAssignments));
                sourceCodeBuilder.AppendLine("}");

                foreach (var deduplicatedMember in deduplicatedDelegatedMembers)
                {
                    delegateMemberService.DelegateMember(deduplicatedMember.Value.Member, deduplicatedMember.Value.MemberName, deduplicatedMember.Value.SetMemberName, deduplicatedMember.ImplementExplicitly, sourceCodeBuilder, usings, deduplicatedMember.Value.Type, null);
                }

                if (getEnumerator != null)
                {
                    sourceCodeBuilder.AppendLine("IEnumerator IEnumerable.GetEnumerator() {");
                    sourceCodeBuilder.Append($"return {getEnumerator.Value.MemberName}.GetEnumerator();");
                    sourceCodeBuilder.AppendLine("}");
                }
                
                sourceCodeBuilder.AppendLine("}\n}\n");

                usings = usings.Distinct().OrderBy(x => x).ToList();

                var filePath = _pathService.SourceCodeRootFolder / (_settings.Folder ?? ".") / $"{className}.g.cs"; 
                results.Add(filePath, $"{string.Join("\n",usings)}\n{sourceCodeBuilder}");
            }
        }
    }
}