using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace FluentSourceGenerators
{
    public class DependencyInjectableExtensionMethodsGenerator : GeneratorBase<DependencyInjectableExtensionMethodsGeneratorSettings>
    {
        private DependencyInjectableExtensionMethodsGeneratorSettings _settings;

        public override void Initialize(DependencyInjectableExtensionMethodsGeneratorSettings settings)
        {
            _settings = settings;
        }
        
        public override ImmutableDictionary<string, string> Generate(CodeIndexerService codeIndexerService)
        {
            var interfaceToDependencyInject = codeIndexerService.GetInterfaceDeclaration(_settings.InterfaceToDependencyInject);
            var interfaceToDependencyInjectSemanticModel = codeIndexerService.GetSemanticModel(interfaceToDependencyInject.SyntaxTree);
            var interfaceToDependencyInjectSymbol = interfaceToDependencyInjectSemanticModel.GetDeclaredSymbol(interfaceToDependencyInject);

            INamedTypeSymbol? typeToAddExtensionMethodsFor = null;
            
            var interfaceToAddExtensionMethodsFor = codeIndexerService.GetInterfaceDeclaration(_settings.TypeToAddExtensionMethodsFor);
            if (interfaceToAddExtensionMethodsFor != null)
            {
                var interfaceToAddExtensionMethodsForSemanticModel = codeIndexerService.GetSemanticModel(interfaceToAddExtensionMethodsFor.SyntaxTree);
                typeToAddExtensionMethodsFor = interfaceToAddExtensionMethodsForSemanticModel.GetDeclaredSymbol(interfaceToAddExtensionMethodsFor) as INamedTypeSymbol;
            }
            else
            {
                var classToAddExtensionMethodsFor =
                    codeIndexerService.GetClassDeclaration(_settings.TypeToAddExtensionMethodsFor);
                var classToAddExtensionMethodsForSemanticModel =
                    codeIndexerService.GetSemanticModel(classToAddExtensionMethodsFor.SyntaxTree);
                typeToAddExtensionMethodsFor =
                    classToAddExtensionMethodsForSemanticModel.GetDeclaredSymbol(classToAddExtensionMethodsFor) as
                        INamedTypeSymbol;
            }

            if (typeToAddExtensionMethodsFor == null)
            {
                Utilities.ThrowException(
                    $"Could not find a class or interface named {_settings.TypeToAddExtensionMethodsFor}");
            } 
            
            // TODO - implement this
            
            return ImmutableDictionary<string, string>.Empty;
        }
    }
}