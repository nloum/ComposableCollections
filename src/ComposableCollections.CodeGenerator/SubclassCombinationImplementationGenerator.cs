using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LiveLinq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComposableCollections.CodeGenerator
{
    public class SubclassCombinationImplementationGenerator : IGenerator<SubclassCombinationImplementationGeneratorSettings>
    {
        private SubclassCombinationImplementationGeneratorSettings _settings;

        public void Initialize(SubclassCombinationImplementationGeneratorSettings settings)
        {
            _settings = settings;
        }

        public ImmutableDictionary<string, string> Generate(IEnumerable<SyntaxTree> syntaxTrees, Func<SyntaxTree, SemanticModel> getSemanticModel)
        {
            var interfaceDeclarations = new Dictionary<string, InterfaceDeclarationSyntax>();
            var interfaceSymbols = new Dictionary<InterfaceDeclarationSyntax, INamedTypeSymbol>();
            var syntaxTreeForEachInterface = new Dictionary<InterfaceDeclarationSyntax, SyntaxTree>();
            var classDeclarations = new Dictionary<string, ClassDeclarationSyntax>();
            var syntaxTreeForEachClass = new Dictionary<ClassDeclarationSyntax, SyntaxTree>();
            var classSymbols = new Dictionary<ClassDeclarationSyntax, INamedTypeSymbol>();

            var syntaxTreesList = syntaxTrees.ToImmutableList();
            
            foreach (var syntaxTree in syntaxTreesList)
            {
                var semanticModel = getSemanticModel(syntaxTree);
                Utilities.TraverseTree(syntaxTree.GetRoot(), node =>
                {
                    if (node is InterfaceDeclarationSyntax interfaceDeclarationSyntax)
                    {
                        interfaceDeclarations.Add(interfaceDeclarationSyntax.Identifier.Text, interfaceDeclarationSyntax);
                        syntaxTreeForEachInterface[interfaceDeclarationSyntax] = syntaxTree;
                        var interfaceSymbolInfo = semanticModel.GetDeclaredSymbol(interfaceDeclarationSyntax);
                        interfaceSymbols[interfaceDeclarationSyntax] = interfaceSymbolInfo;
                    }

                    if (node is ClassDeclarationSyntax classDeclarationSyntax)
                    {
                        if (!classDeclarationSyntax.Modifiers.Any(SyntaxKind.PublicKeyword))
                        {
                            return;
                        }
                        classDeclarations[classDeclarationSyntax.Identifier.Text] = classDeclarationSyntax;
                        syntaxTreeForEachClass[classDeclarationSyntax] = syntaxTree;
                        var classSymbolInfo = semanticModel.GetDeclaredSymbol(classDeclarationSyntax);
                        classSymbols[classDeclarationSyntax] = classSymbolInfo;
                    }
                });
            }
            
            var result = new Dictionary<string, string>();

            var theClass = classDeclarations[_settings.BaseClass];
            var theClassSemanticModel = getSemanticModel(syntaxTreeForEachClass[theClass]);

            var baseInterfaceName = theClass.BaseList.Types.Select(baseType =>
            {
                var key = baseType.Type.ToString();
                if (key.Contains("<"))
                {
                    key = key.Substring(0, key.IndexOf('<'));
                }

                if (interfaceDeclarations.ContainsKey(key))
                {
                    return key;
                }

                return "";
            }).First(str => !string.IsNullOrWhiteSpace(str));
            var baseInterface = interfaceDeclarations[baseInterfaceName];
            var baseInterfaceSymbol = interfaceSymbols[baseInterface];

            var subInterfaces = new List<InterfaceDeclarationSyntax>();

            foreach(var interfaceDeclaration in interfaceDeclarations)
            {
                if (Utilities.IsBaseInterface(interfaceSymbols[interfaceDeclaration.Value], baseInterfaceSymbol))
                {
                    subInterfaces.Add(interfaceDeclaration.Value);
                }
            }

            if (subInterfaces.Contains(baseInterface))
            {
                subInterfaces.Remove(baseInterface);
            }

            var delegateMemberService = new DelegateMemberService();
            var memberDeduplicationService = new MemberDeduplicationService();

            var constructors = theClass.Members.OfType<ConstructorDeclarationSyntax>()
                .ToImmutableList();
            var baseInterfaces = Utilities.GetBaseInterfaces(theClassSemanticModel.GetDeclaredSymbol(theClass));
            var baseMembers = baseInterfaces.SelectMany(baseInterface => baseInterface.GetMembers());
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
                    subClassName = Regex.Replace(subClassName, modifier.Search, modifier.Replacement);
                }
                
                usings.AddRange(Utilities.GetDescendantsOfType<UsingDirectiveSyntax>(subInterface.SyntaxTree.GetRoot())
                    .Select(us => $"using {us.Name};\n"));
                usings.AddRange(Utilities.GetDescendantsOfType<UsingDirectiveSyntax>(theClass.SyntaxTree.GetRoot())
                    .Select(us => $"using {us.Name};\n"));

                classDefinition.Add($"namespace {_settings.Namespace} {{\n");
                classDefinition.Add($"public class {subClassName}{subInterface.TypeParameterList} : {theClass.Identifier}{theClass.TypeParameterList}, {subInterface.Identifier}{subInterface.TypeParameterList} {{\n");

                var stuffAddedForSubInterface =
                    Utilities.GetBaseInterfaces(getSemanticModel(syntaxTreeForEachInterface[subInterface]).GetDeclaredSymbol(subInterface))
                        .Except(Utilities.GetBaseInterfaces(theClassSemanticModel.GetDeclaredSymbol(theClass)));

                var constructor = constructors.Single();
                
                var adaptedParameter = constructor.ParameterList.Parameters.First();

                var constructorParameters = new List<string>();
                var baseConstructorArguments = new List<string>();
                 
                var desiredAdaptedBaseInterfaces = Utilities
                    .GetBaseInterfaces(
                        theClassSemanticModel.GetSymbolInfo(adaptedParameter.Type).Symbol as INamedTypeSymbol)
                    .Concat(stuffAddedForSubInterface).Select(x => x.ToString()).ToImmutableHashSet();

                InterfaceDeclarationSyntax bestAdaptedInterface = null;

                foreach (var iface in interfaceDeclarations.Values)
                {
                    var ifaceBaseInterfaces = Utilities.GetBaseInterfaces(getSemanticModel(syntaxTreeForEachInterface[iface]).GetDeclaredSymbol(iface)).ToImmutableHashSet();
                    if (desiredAdaptedBaseInterfaces.Count == ifaceBaseInterfaces.Count
                        && ifaceBaseInterfaces.All(ifaceBaseInterface =>
                            desiredAdaptedBaseInterfaces.Contains(ifaceBaseInterface.ToString())))
                    {
                        bestAdaptedInterface = iface;
                        break;
                    }
                }
                
                classDefinition.Add($"private readonly {bestAdaptedInterface.Identifier}{subInterface.TypeParameterList} _adapted;\n");
                constructorParameters.Add($"{bestAdaptedInterface.Identifier}{subInterface.TypeParameterList} adapted");
                baseConstructorArguments.Add("adapted");

                for (var i = 1; i < constructor.ParameterList.Parameters.Count; i++)
                {
                    var parameter = constructor.ParameterList.Parameters[i];
                    constructorParameters.Add($"{parameter.Type} {parameter.Identifier}");
                    baseConstructorArguments.Add(parameter.Identifier.ToString());
                }
                
                classDefinition.Add($"public {subClassName}(");
                classDefinition.Add(string.Join(", ", constructorParameters));
                classDefinition.Add(") : base(" + string.Join(", ", baseConstructorArguments) + ") {\n");
                classDefinition.Add("_adapted = adapted;");
                classDefinition.Add("}\n");
                
                foreach (var member in memberDeduplicationService.GetDeduplicatedMembers(getSemanticModel(syntaxTreeForEachInterface[bestAdaptedInterface]).GetDeclaredSymbol(bestAdaptedInterface)))
                {
                    if (member.Duplicates.All(duplicate => baseMemberImplementationProfiles.Contains(memberDeduplicationService.GetImplementationProfile(duplicate))))
                    {
                        continue;
                    }

                    // var implementExplicitly = member.Duplicates.Any(duplicate =>
                    //     baseMemberExplicitImplementationProfiles.Contains(
                    //         memberDeduplicationService.GetExplicitImplementationProfile(duplicate)));
                    
                    var sourceCodeBuilder = new StringBuilder();
                    var shouldOverride = member.Duplicates.Any(duplicate => duplicate.ContainingType.TypeKind == TypeKind.Class);
                    delegateMemberService.DelegateMember(member.Symbol, "_adapted", member.ImplementExplicitly, sourceCodeBuilder, usings, shouldOverride);
                    classDefinition.Add(sourceCodeBuilder + "\n");
                }

                classDefinition.Add("}\n}\n");
                result[subClassName + ".g.cs"] = string.Join("", usings.Concat(classDefinition));
            }

            return result.ToImmutableDictionary();
        }
    }
}