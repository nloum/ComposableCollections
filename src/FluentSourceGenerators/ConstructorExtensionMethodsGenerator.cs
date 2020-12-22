using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FluentSourceGenerators
{
    public class ConstructorExtensionMethodsGenerator : GeneratorBase<ConstructorExtensionMethodsGeneratorSettings>
    {
        private ConstructorExtensionMethodsGeneratorSettings _settings;

        public override void Initialize(ConstructorExtensionMethodsGeneratorSettings settings)
        {
            _settings = settings;
        }

        public override ImmutableDictionary<string, string> Generate(CodeIndexerService codeIndexerService)
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
                if (aClassSemanticModel == null)
                {
                    Utilities.ThrowException($"There is no semantic model for the class {aClass.Identifier}, which is defined in the syntax. This implies a logic error in FluentSourceGenerators.");
                }
                var aClassSymbol = aClassSemanticModel?.GetDeclaredSymbol(aClass);
                if (aClassSymbol == null)
                {
                    Utilities.ThrowException($"There is no symbol for the class {aClass.Identifier}, which is defined in the syntax. This implies a logic error in FluentSourceGenerators.");
                }
                usings.Add($"using {aClassSymbol.ContainingNamespace};\n");

                foreach (var constructor in aClass.Members.OfType<ConstructorDeclarationSyntax>().Where(constructor => constructor.Modifiers.Any(SyntaxKind.PublicKeyword)))
                {
                    constructors.Add( Tuple.Create(aClass, constructor, aClassSemanticModel!) );
                }
            }
            
            var memberDeduplicationService = new MemberDeduplicationService();
            var deduplicatedConstructors = memberDeduplicationService.DeduplicateMembers(constructors, x => x.Item3.GetDeclaredSymbol(x.Item2)!);

            foreach (var deduplicatedMember in deduplicatedConstructors)
            {
                var (aClass, constructor, aClassSemanticModel) = deduplicatedMember.Value;
                
                var constructorArguments = string.Join(", ",
                    constructor.ParameterList.Parameters.Select(parameter => parameter.Identifier.Text));

                var parameters = string.Join(", ",
                    constructor.ParameterList.Parameters.Select(parameter =>
                        $"{parameter.Type} {parameter.Identifier.Text}"));
                    
                usings.AddRange(constructor.ParameterList.Parameters
                    .Select(parameter => aClassSemanticModel.GetSymbolInfo(parameter.Type!).Symbol!)
                    .Where(symbol => symbol != null)
                    .Select(symbol => $"using {symbol.ContainingNamespace};\n"));

                var theInterface = aClass!.BaseList?.Types.FirstOrDefault(type =>
                {
                    var symbol = aClassSemanticModel.GetSymbolInfo(type.Type).Symbol as INamedTypeSymbol;
                    return symbol != null && symbol.TypeKind == TypeKind.Interface;
                });
                
                var typeArgs = aClass!.TypeParameterList != null ? string.Join(", ",
                    aClass!.TypeParameterList!.Parameters.Select(parameter => parameter.Identifier.Text)) : String.Empty;
                if (!string.IsNullOrWhiteSpace(typeArgs))
                {
                    typeArgs = $"<{typeArgs}>";
                }

                var returnType = theInterface != null ? theInterface?.Type.ToString() : aClass!.Identifier + typeArgs;
                extensionMethods.Add(aClassSemanticModel?.GetDeclaredSymbol(constructor)?.GetDocumentationCommentXml() + "\n" ?? "");
                extensionMethods.Add($"public static {returnType} {_settings.ExtensionMethodName}{typeArgs}(this {parameters}) {{\n");
                extensionMethods.Add($"return new {aClass.Identifier}{typeArgs}({constructorArguments});");
                extensionMethods.Add("}\n");
            }
            
            extensionMethods.Add("}\n}\n");

            return ImmutableDictionary<string, string>.Empty
                .Add($"{_settings.ExtensionMethodName}Extensions.g.cs",
                    string.Join("", usings.Distinct().OrderBy(x => x).Concat(extensionMethods)));
        }
    }
}