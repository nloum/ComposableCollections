using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using IoFluently;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComposableCollections.CodeGenerator
{
    public class ConstructorExtensionMethodsGenerator : GeneratorBase<ConstructorExtensionMethodsGeneratorSettings>
    {
        private ConstructorExtensionMethodsGeneratorSettings _settings;
        private IPathService _pathService;

        public ConstructorExtensionMethodsGenerator(IPathService pathService)
        {
            _pathService = pathService;
        }

        public override void Initialize(ConstructorExtensionMethodsGeneratorSettings settings)
        {
            _settings = settings;
        }

        public override ImmutableDictionary<AbsolutePath, string> Generate(CodeIndexerService codeIndexerService)
        {
            var usings = new List<string>();
            
            var theClasses = _settings.BaseClasses.SelectMany(theClassName =>
            {
                var theClass = codeIndexerService.GetClassDeclaration(theClassName);
                var theClassSemanticModel = codeIndexerService.GetSemanticModel(theClass.SyntaxTree);
                var theClassSymbol = theClassSemanticModel.GetDeclaredSymbol(theClass);
                
                return codeIndexerService.GetAllClassDeclarations().Where(classDecl =>
                {
                    var usingStatementSyntaxes = Utilities.GetDescendantsOfType<UsingDirectiveSyntax>(classDecl.SyntaxTree.GetRoot());
                    usings.AddRange(usingStatementSyntaxes
                        .Select(us => us.ToString() + "\n"));
                    var aClassSymbol = codeIndexerService.GetSemanticModel(classDecl.SyntaxTree).GetDeclaredSymbol(classDecl);
                    return Utilities.IsBaseClass(aClassSymbol, theClassSymbol);
                });
            }).ToImmutableDictionary(x => x.Identifier.Text);

            var extensionMethods = new List<string>();
            
            extensionMethods.Add($"namespace {_settings.Namespace} {{\n");
            extensionMethods.Add($"public static class {_settings.ExtensionMethodName}Extensions {{\n");

            var constructors = new List<Tuple<ClassDeclarationSyntax, ConstructorDeclarationSyntax, SemanticModel>>();
            
            foreach (var aClass in theClasses.Values)
            {
                var aClassSemanticModel = codeIndexerService.GetSemanticModel(aClass.SyntaxTree);
                usings.Add($"using {aClassSemanticModel.GetDeclaredSymbol(aClass).ContainingNamespace};\n");

                foreach (var constructor in aClass.Members.OfType<ConstructorDeclarationSyntax>().Where(constructor => constructor.Modifiers.Any(SyntaxKind.PublicKeyword)))
                {
                    constructors.Add( Tuple.Create(aClass, constructor, aClassSemanticModel) );
                }
            }
            
            var memberDeduplicationService = new MemberDeduplicationService();
            var deduplicatedConstructors = memberDeduplicationService.DeduplicateMembers(constructors, x => x.Item3.GetDeclaredSymbol(x.Item2));

            foreach (var deduplicatedMember in deduplicatedConstructors)
            {
                var (aClass, constructor, aClassSemanticModel) = deduplicatedMember.Value;
                
                var constructorArguments = string.Join(", ",
                    constructor.ParameterList.Parameters.Select(parameter => parameter.Identifier.Text));

                var parameters = string.Join(", ",
                    constructor.ParameterList.Parameters.Select(parameter =>
                        $"{parameter.Type} {parameter.Identifier.Text}"));
                    
                usings.AddRange(constructor.ParameterList.Parameters
                    .Select(parameter => aClassSemanticModel.GetSymbolInfo(parameter.Type).Symbol)
                    .Where(symbol => symbol != null)
                    .Select(symbol => $"using {symbol.ContainingNamespace};\n"));

                var theInterface = aClass.BaseList.Types.First(type =>
                {
                    var symbol = aClassSemanticModel.GetSymbolInfo(type.Type).Symbol as INamedTypeSymbol;
                    return symbol.TypeKind == TypeKind.Interface;
                });
                    
                var typeArgs = string.Join(", ",
                    aClass.TypeParameterList.Parameters.Select(parameter => parameter.Identifier.Text));
                if (!string.IsNullOrWhiteSpace(typeArgs))
                {
                    typeArgs = $"<{typeArgs}>";
                }

                extensionMethods.Add($"public static {theInterface.Type} {_settings.ExtensionMethodName}{typeArgs}(this {parameters}) {{\n");
                extensionMethods.Add($"return new {aClass.Identifier}{typeArgs}({constructorArguments});");
                extensionMethods.Add("}\n");
            }
            
            extensionMethods.Add("}\n}\n");

            return ImmutableDictionary<AbsolutePath, string>.Empty
                .Add(_pathService.SourceCodeRootFolder / (_settings.Folder ?? ".") / $"{_settings.ExtensionMethodName}Extensions.g.cs",
                    string.Join("", usings.Distinct().OrderBy(x => x).Concat(extensionMethods)));
        }
    }
}