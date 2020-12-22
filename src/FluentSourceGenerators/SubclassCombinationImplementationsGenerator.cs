using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;

namespace FluentSourceGenerators
{
    public class SubclassCombinationImplementationsGenerator : GeneratorBase<SubclassCombinationImplementationsGeneratorSettings>
    {
        private SubclassCombinationImplementationsGeneratorSettings _settings;

        public override void Initialize(SubclassCombinationImplementationsGeneratorSettings settings)
        {
            _settings = settings;
        }

        public override ImmutableDictionary<string, string> Generate(CodeIndexerService codeIndexerService)
        {
            var result = new Dictionary<string, string>();

            var theClass = codeIndexerService.GetClassDeclaration(_settings.BaseClass);
            if (theClass == null)
            {
                throw Utilities.MakeException(
                    $"The class {_settings.BaseClass} could not be found in the syntax");
            }
            var theClassSemanticModel = codeIndexerService.GetSemanticModel(theClass!.SyntaxTree)
                ?? throw Utilities.MakeException($"The class {_settings.BaseClass} has no semantic model");

            if (theClass.BaseList == null || theClass.BaseList.Types.Count == 0)
            {
                throw Utilities.MakeException($"The class {_settings.BaseClass} needs to implement an interface" +
                                                    $" for the {nameof(SubclassCombinationImplementationsGenerator)} to work");
            }
            
            var baseInterfaceSymbol = theClass!.BaseList?.Types.Select(baseType =>
            {
                var key = baseType.Type.ToString();
                if (key.Contains("<"))
                {
                    key = key.Substring(0, key.IndexOf('<'));
                }

                var result = theClassSemanticModel!.GetDeclaredSymbol(baseType.Type) as INamedTypeSymbol;
                if (result == null)
                {
                    throw Utilities.MakeException($"The class {_settings.BaseClass} inherits from {key} but {key} is not defined in {_settings.BaseClass}'s semantic model.");
                }
                return result;
            }).FirstOrDefault(s => s!.TypeKind == TypeKind.Interface);

            if (baseInterfaceSymbol == null)
            {
                throw Utilities.MakeException($"The class {_settings.BaseClass} needs to implement an interface" +
                                                    $" for the {nameof(SubclassCombinationImplementationsGenerator)} to work");
            }

            var subInterfaces = new List<InterfaceDeclarationSyntax>();

            foreach(var interfaceDeclaration in codeIndexerService.GetAllInterfaceDeclarations())
            {
                var interfaceSymbol = codeIndexerService.GetSymbol(interfaceDeclaration);
                if (interfaceSymbol == null)
                {
                    throw Utilities.MakeException($"The interface {interfaceDeclaration.Identifier} has no symbol");
                }
                if (Utilities.IsBaseInterface(interfaceSymbol, baseInterfaceSymbol))
                {
                    subInterfaces.Add(interfaceDeclaration);
                }
            }

            for (var i = 0; i < subInterfaces.Count; i++)
            {
                var subInterface = subInterfaces[i];
                if (subInterface.Identifier.Text == baseInterfaceSymbol!.Name)
                {
                    subInterfaces.RemoveAt(i);
                    break;
                }
            }
            
            var delegateMemberService = new DelegateMemberService();
            var memberDeduplicationService = new MemberDeduplicationService();

            var constructors = theClass.Members.OfType<ConstructorDeclarationSyntax>()
                .ToImmutableList();
            var baseInterfaces = Utilities.GetBaseInterfaces(theClassSemanticModel!.GetDeclaredSymbol(theClass));
            var baseMembers = baseInterfaces.SelectMany(aBaseInterface => aBaseInterface.GetMembers()).ToImmutableList();
            var baseMemberExplicitImplementationProfiles =
                baseMembers.Select(baseMember => memberDeduplicationService.GetExplicitImplementationProfile(baseMember)).Distinct().ToImmutableHashSet();
            var baseMemberImplementationProfiles =
                baseMembers.Select(baseMember => memberDeduplicationService.GetImplementationProfile(baseMember)).Distinct().ToImmutableHashSet();
            
            foreach (var subInterface in subInterfaces)
            {
                var usings = new List<string>();
                var classDefinition = new List<string>();

                var subClassName = subInterface.Identifier.Text.Substring(1);
                foreach (var modifier in _settings.ClassNameModifiers)
                {
                    subClassName = Regex.Replace(subClassName, modifier.Search ?? "", modifier.Replace ?? "");
                }

                if (_settings.ClassNameBlacklist.Any(classNameBlacklistItem =>
                    Regex.IsMatch(subClassName, classNameBlacklistItem)))
                {
                    continue;
                }
                
                if (!_settings.ClassNameWhitelist.All(classNameWhitelistItem =>
                    Regex.IsMatch(subClassName, classNameWhitelistItem)))
                {
                    continue;
                }

                usings.AddRange(Utilities.GetDescendantsOfType<UsingDirectiveSyntax>(subInterface.SyntaxTree.GetRoot())
                    .Select(us => $"using {us.Name};\n"));
                usings.AddRange(Utilities.GetDescendantsOfType<UsingDirectiveSyntax>(theClass.SyntaxTree.GetRoot())
                    .Select(us => $"using {us.Name};\n"));

                classDefinition.Add($"\nnamespace {_settings.Namespace} {{\n");
                var subInterfaceTypeArgs =
                    subInterface.TypeParameterList != null ? string.Join(", ", subInterface!.TypeParameterList!.Parameters.Select(p => p.Identifier.Text))
                        : "";
                if (!string.IsNullOrWhiteSpace(subInterfaceTypeArgs))
                {
                    subInterfaceTypeArgs = $"<{subInterfaceTypeArgs}>";
                }
                classDefinition.Add("/// <summary>\n");
                classDefinition.Add($"/// Extends <cref see=\"{_settings.BaseClass}\" /> to implement <cref see=\"{subInterface.Identifier.Text}\" />.\n");
                classDefinition.Add("/// </summary>\n");
                classDefinition.Add($"public class {subClassName}{theClass.TypeParameterList} : {theClass.Identifier}{theClass.TypeParameterList}, {subInterface.Identifier}{subInterfaceTypeArgs} {{\n");

                var stuffAddedForSubInterface =
                    Utilities.GetBaseInterfaces(codeIndexerService.GetSemanticModel(subInterface.SyntaxTree).GetDeclaredSymbol(subInterface))
                        .Except(Utilities.GetBaseInterfaces(theClassSemanticModel.GetDeclaredSymbol(theClass)));

                var adaptedParameter = constructors.First().ParameterList.Parameters.First();
                var desiredAdaptedBaseInterfaces = Utilities
                    .GetBaseInterfaces(
                        theClassSemanticModel.GetSymbolInfo(adaptedParameter.Type).Symbol as INamedTypeSymbol)
                    .Concat(stuffAddedForSubInterface).Select(x => x.ToString()).ToImmutableHashSet();
                var adaptedParameterTypeArgs = "";
                var tmp = adaptedParameter.Type.ToString();
                if (tmp.Contains("<"))
                {
                    adaptedParameterTypeArgs = tmp.Substring(tmp.IndexOf('<'));
                }

                if (_settings.AllowDifferentTypeParameters)
                {
                    desiredAdaptedBaseInterfaces = desiredAdaptedBaseInterfaces.Select(Utilities.GetWithoutTypeArguments).ToImmutableHashSet();
                }

                InterfaceDeclarationSyntax? bestAdaptedInterface = null;

                foreach (var iface in codeIndexerService.GetAllInterfaceDeclarations())
                {
                    var ifaceSemanticModel = codeIndexerService.GetSemanticModel(iface.SyntaxTree);
                    if (ifaceSemanticModel == null)
                    {
                        throw Utilities.MakeException(
                            $"The interface {iface.Identifier.Text} has no semantic model");
                    }
                    var ifaceSymbol = ifaceSemanticModel.GetDeclaredSymbol(iface);
                    if (ifaceSymbol == null)
                    {
                        throw Utilities.MakeException($"The interface {iface.Identifier.Text} has no symbol in the semantic model");
                    }
                    var ifaceBaseInterfaces = Utilities
                        .GetBaseInterfaces(ifaceSymbol)
                        .Select(x => x.ToString()).ToImmutableHashSet();
                    
                    if (_settings.AllowDifferentTypeParameters)
                    {
                         ifaceBaseInterfaces = ifaceBaseInterfaces.Select(Utilities.GetWithoutTypeArguments).ToImmutableHashSet();
                    }
                    
                    if (desiredAdaptedBaseInterfaces.Count == ifaceBaseInterfaces.Count)
                    {
                        if (ifaceBaseInterfaces.All(ifaceBaseInterface =>
                            desiredAdaptedBaseInterfaces.Contains(ifaceBaseInterface)))
                        {
                            bestAdaptedInterface = iface;
                            break;
                        }
                    }
                }

                if (bestAdaptedInterface == null)
                {
                    throw Utilities.MakeException($"Cannot find the best interface to adapt for {adaptedParameter.Type}");
                }
                
                classDefinition.Add(
                    $"private readonly {bestAdaptedInterface!.Identifier}{adaptedParameterTypeArgs} _adapted;\n");

                foreach (var constructor in constructors)
                {
                    var constructorParameters = new List<string>();
                    var baseConstructorArguments = new List<string>();

                    constructorParameters.Add(
                        $"{bestAdaptedInterface.Identifier}{adaptedParameterTypeArgs} adapted");
                    baseConstructorArguments.Add("adapted");

                    for (var i = 1; i < constructor.ParameterList.Parameters.Count; i++)
                    {
                        var parameter = constructor.ParameterList.Parameters[i];
                        constructorParameters.Add($"{parameter.Type} {parameter.Identifier}");
                        baseConstructorArguments.Add(parameter.Identifier.ToString());
                    }
                    
                    classDefinition.Add(theClassSemanticModel.GetDeclaredSymbol(constructor)?.GetDocumentationCommentXml() + "\n" ?? "");
                    classDefinition.Add($"public {subClassName}(");
                    classDefinition.Add(string.Join(", ", constructorParameters));
                    classDefinition.Add(") : base(" + string.Join(", ", baseConstructorArguments) + ") {\n");
                    classDefinition.Add("_adapted = adapted;");
                    classDefinition.Add("}\n");
                }

                foreach (var member in memberDeduplicationService.GetDeduplicatedMembers(codeIndexerService.GetSemanticModel(bestAdaptedInterface.SyntaxTree).GetDeclaredSymbol(bestAdaptedInterface)))
                {
                    if (member.Duplicates.All(duplicate => baseMemberImplementationProfiles.Contains(memberDeduplicationService.GetImplementationProfile(duplicate))))
                    {
                        continue;
                    }

                    usings.AddRange(member.Duplicates.SelectMany(duplicate => duplicate.DeclaringSyntaxReferences).SelectMany(syntaxRef =>
                    {
                        var root = syntaxRef.SyntaxTree.GetRoot();
                        var usingDirectives = Utilities.GetDescendantsOfType<UsingDirectiveSyntax>(root);
                        return usingDirectives.Select(usingDirective => $"{usingDirective}\n");
                    }));
                    
                    var sourceCodeBuilder = new StringBuilder();
                    var shouldOverride = member.Duplicates.Any(duplicate => duplicate.ContainingType.TypeKind == TypeKind.Class);
                    delegateMemberService.DelegateMember(member.Value, "_adapted", null, member.ImplementExplicitly, sourceCodeBuilder, usings, DelegateType.DelegateObject, shouldOverride);
                    classDefinition.Add(sourceCodeBuilder + "\n");
                }

                classDefinition.Add("}\n}\n");
                result[subClassName + ".g.cs"] = string.Join("", usings.Distinct().Concat(classDefinition));
            }

            return result.ToImmutableDictionary();
        }
    }
}