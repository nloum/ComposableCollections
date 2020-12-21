using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FluentSourceGenerators
{
    public class AnonymousImplementationsGenerator : GeneratorBase<AnonymousImplementationsGeneratorSettings>
    {
        private AnonymousImplementationsGeneratorSettings _settings;

        public override void Initialize(AnonymousImplementationsGeneratorSettings settings)
        {
            _settings = settings;
        }

        public override ImmutableDictionary<string, string> Generate(CodeIndexerService codeIndexerService)
        {
            var results = new Dictionary<string, string>();

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

                var filePath = $"{className}.g.cs"; 
                results.Add(filePath, $"{string.Join("\n",usings)}\n{sourceCodeBuilder}");
            }

            return results.ToImmutableDictionary();
        }

        private void GetInterfacesToImplementWith(INamedTypeSymbol superInterface,
            List<Parameter> parameters,
            List<MemberToBeDelegated> membersToBeDelegated)
        {
            if (_settings.AllowedArguments?.Any(allowedArgument => Regex.IsMatch(superInterface.Name, allowedArgument)) == true)
            {
                var type = superInterface.Name;
                if (superInterface.TypeArguments.Length > 0)
                {
                    type += $"<{string.Join(", ", superInterface.TypeArguments.Select(typeArgument => typeArgument.Name))}>";
                }
                var matchingParameter = parameters.FirstOrDefault(parameter => parameter.Type == type);
                if (matchingParameter == null)
                {
                    var parameterName = Utilities.GenerateVariableName(superInterface.Name, true);
                    matchingParameter = new Parameter(parameterName, "_" + parameterName, type, DelegateType.DelegateObject);
                    parameters.Add(matchingParameter);
                }

                var interfaces = Utilities.GetBaseInterfaces(superInterface).ToImmutableList();
                foreach (var member in interfaces.SelectMany(iface => iface.GetMembers()))
                {
                    membersToBeDelegated.Add(new MemberToBeDelegated(matchingParameter.MemberName, type, member, DelegateType.DelegateObject, ImmutableList<INamedTypeSymbol>.Empty.Add(member.ContainingType)));
                }
            }
            else
            {
                if (superInterface is IErrorTypeSymbol errorTypeSymbol)
                {
                    int a = 3;
                }
                foreach (var member in superInterface.GetMembers())
                {
                    if (member is IMethodSymbol methodSymbol)
                    {
                        if (member.Name.StartsWith("get_") || member.Name.StartsWith("set_"))
                        {
                            continue;
                        }

                        CalculateMethodDelegationDetails(parameters, membersToBeDelegated, methodSymbol);
                    }
                    else if (member is IPropertySymbol propertySymbol)
                    {
                        if (propertySymbol.IsReadOnly && _settings.CachePropertyValues)
                        {
                                var parameterName = propertySymbol.Name.Camelize();
                                parameters.Add(new Parameter(parameterName, propertySymbol.Name, propertySymbol.Type.ToString(), DelegateType.AutoProperty));
                                membersToBeDelegated.Add(new MemberToBeDelegated(null, null, propertySymbol, DelegateType.AutoProperty, ImmutableList<INamedTypeSymbol>.Empty.Add(superInterface)));
                        }
                        else
                        {
                            CalculatePropertyDelegationDetails(parameters, membersToBeDelegated, propertySymbol);
                        }
                    }
                }

                foreach (var baseType in superInterface.Interfaces)
                {
                    GetInterfacesToImplementWith(baseType, parameters, membersToBeDelegated);
                }
            }
        }
        
        private static void CalculatePropertyDelegationDetails(List<Parameter> parameters, List<MemberToBeDelegated> membersToBeDelegated, IPropertySymbol propertySymbol)
        {
            if (propertySymbol.IsIndexer)
            {
                throw new InvalidOperationException();
            }

            Parameter getter = null, setter = null;
            
            if (!propertySymbol.IsWriteOnly)
            {
                var parameterType = $"Func<{propertySymbol.Type}>";
                var parameterName = "get" + propertySymbol.Name;
                getter = GetParameter(parameters, parameterType, parameterName, p => $"_{p}", DelegateType.ActionOrFunc);
            }
            if (!propertySymbol.IsReadOnly)
            {
                var parameterType = $"Action<{propertySymbol.Type}>";
                var parameterName = "set" + propertySymbol.Name;
                setter = GetParameter(parameters, parameterType, parameterName, p => $"_{p}", DelegateType.ActionOrFunc);
            }

            if (propertySymbol.IsReadOnly)
            {
                membersToBeDelegated.Add(new MemberToBeDelegated(getter.MemberName, getter.Type, propertySymbol, DelegateType.ActionOrFunc, ImmutableList<INamedTypeSymbol>.Empty.Add(propertySymbol.ContainingType)));
            }
            else if (propertySymbol.IsWriteOnly)
            {
                membersToBeDelegated.Add(new MemberToBeDelegated(null, null, setter.MemberName, setter.Type, propertySymbol, DelegateType.ActionOrFunc, ImmutableList<INamedTypeSymbol>.Empty.Add(propertySymbol.ContainingType)));
            }
            else
            {
                membersToBeDelegated.Add(new MemberToBeDelegated(getter.MemberName, getter.Type, setter.MemberName, setter.Type, propertySymbol, DelegateType.ActionOrFunc, ImmutableList<INamedTypeSymbol>.Empty.Add(propertySymbol.ContainingType)));
            }
        }

        private static Parameter GetParameter(List<Parameter> parameters, string parameterType, string parameterName, Func<string, string> calculateMemberNameFromParameterName,
            DelegateType delegateType)
        {
            var parameterIndex = -1;
            Parameter parameter = null;
            while (true)
            {
                parameterIndex++;
                var tmpParameterName = parameterName + (parameterIndex > 0 ? parameterIndex.ToString() : "");

                parameter = parameters.FirstOrDefault(p => p.ParameterName == tmpParameterName);
                if (parameter == null || parameter.Type == parameterType)
                {
                    parameterName = tmpParameterName;
                    break;
                }
            }

            if (parameter == null)
            {
                parameter = new Parameter(parameterName, calculateMemberNameFromParameterName(parameterName),
                    parameterType, DelegateType.ActionOrFunc);
                parameters.Add(parameter);
            }
            
            return parameter;
        }

        private static void CalculateMethodDelegationDetails(List<Parameter> parameters, List<MemberToBeDelegated> membersToBeDelegated, IMethodSymbol methodSymbol)
        {
            if (methodSymbol.ReturnsVoid)
            {
                var parameterTypes = string.Join(", ", methodSymbol.Parameters.Select(parameter => parameter.Type.ToString()));
                var parameterType = methodSymbol.Parameters.Length == 0
                    ? "Action"
                    : $"Action<{parameterTypes}>";
                var parameterName = methodSymbol.Name.Camelize();
                var preExistingParameter = GetParameter(parameters, parameterType, parameterName, p => $"_{p}", DelegateType.ActionOrFunc);
                membersToBeDelegated.Add(new MemberToBeDelegated(preExistingParameter.MemberName, parameterType, methodSymbol, DelegateType.ActionOrFunc, ImmutableList<INamedTypeSymbol>.Empty.Add(methodSymbol.ContainingType)));
            }
            else
            {
                var parameterTypes = string.Join(", ",
                    methodSymbol.Parameters.Select(parameter => parameter.Type.ToString())
                        .Concat(new[] {methodSymbol.ReturnType.ToString()}));
                var parameterType = $"Func<{parameterTypes}>";
                var parameterName = methodSymbol.Name.Camelize();
                var preExistingParameter = GetParameter(parameters, parameterType, parameterName, p => $"_{p}", DelegateType.ActionOrFunc);
                membersToBeDelegated.Add(new MemberToBeDelegated(preExistingParameter.MemberName, parameterType, methodSymbol, DelegateType.ActionOrFunc, ImmutableList<INamedTypeSymbol>.Empty.Add(methodSymbol.ContainingType)));
            }
        }

        private class Parameter
        {
            public Parameter(string parameterName, string memberName, string type, DelegateType delegateType)
            {
                ParameterName = parameterName;
                MemberName = memberName;
                Type = type;
                DelegateType = delegateType;
            }

            public string ParameterName { get; }
            public string MemberName { get; }
            public string Type { get; }
            public DelegateType DelegateType { get; }

            public string ToMemberDeclaration()
            {
                return $"private readonly {Type} {MemberName};";
            }
        }
        
        private class MemberToBeDelegated
        {
            public MemberToBeDelegated(string memberName, string fieldType, ISymbol member, DelegateType type, ImmutableList<INamedTypeSymbol> containingTypes)
            {
                MemberName = memberName;
                FieldType = fieldType;
                SetMemberName = null;
                SetFieldType = null;
                Member = member;
                Type = type;
                ContainingTypes = containingTypes;
            }

            public MemberToBeDelegated(string memberName, string fieldType, string setMemberName, string setFieldType, ISymbol member, DelegateType type, ImmutableList<INamedTypeSymbol> containingTypes)
            {
                MemberName = memberName;
                FieldType = fieldType;
                SetMemberName = setMemberName;
                SetFieldType = setFieldType;
                Member = member;
                Type = type;
                ContainingTypes = containingTypes;
            }

            public string MemberName { get; }
            public string FieldType { get; }
            public string SetMemberName { get; }
            public string SetFieldType { get; }
            public ISymbol Member { get; }
            public DelegateType Type { get; }
            public ImmutableList<INamedTypeSymbol> ContainingTypes { get; }

            public override string ToString()
            {
                return $"{FieldType}.{Member.Name}";
            }
        }
    }
}