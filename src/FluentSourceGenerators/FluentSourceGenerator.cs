using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;

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
            var configurationFile = context.AdditionalFiles.First(
                additionalFile => additionalFile.Path.Contains("FluentApiSourceGenerator.xml"));

            var lastSeparatorIndex = configurationFile.Path.IndexOf('@');
            var projectFolder = configurationFile.Path.Substring(0, lastSeparatorIndex);
            var fileName = configurationFile.Path.Substring(configurationFile.Path.IndexOf('(')).Trim(')', '(');
            var configurationFilePath = Path.Combine(projectFolder, fileName);

            var compilation = context.Compilation;
            Action<string, string> addSource = context.AddSource;
            
            Execute(configurationFilePath, compilation, addSource);
        }

        public static void Execute(string configurationFilePath, Compilation compilation,
            Action<string, string> addSource)
        {
            Configuration? configuration = null;
            using (var streamReader = new StreamReader(configurationFilePath))
            {
                configuration = (Configuration) new XmlSerializer(typeof (Configuration)).Deserialize(streamReader);
            }

            Execute(configuration, compilation, addSource);
        }

        public static void Execute(Configuration configuration, Compilation compilation, Action<string, string> addSource)
        {
            var options = (CSharpParseOptions) compilation.SyntaxTrees.First().Options;
            
            foreach (var codeGeneratorSettings in configuration.CodeGenerators)
            {
                var codeIndexerService =
                    new CodeIndexerService(compilation.SyntaxTrees, tree => compilation.GetSemanticModel(tree));

                GeneratorBase generator;
                if (codeGeneratorSettings is AnonymousImplementationsGeneratorSettings anonymousImplementationsGeneratorSettings
                )
                {
                    generator = new AnonymousImplementationsGenerator();
                    generator.NonGenericInitialize(anonymousImplementationsGeneratorSettings);
                }
                else if (codeGeneratorSettings is CombinationInterfacesGeneratorSettings combinationInterfacesGeneratorSettings)
                {
                    generator = new CombinationInterfacesGenerator();
                    generator.NonGenericInitialize(combinationInterfacesGeneratorSettings);
                }
                else if (codeGeneratorSettings is ConstructorExtensionMethodsGeneratorSettings
                    constructorExtensionMethodsGeneratorSettings)
                {
                    generator = new ConstructorExtensionMethodsGenerator();
                    generator.NonGenericInitialize(constructorExtensionMethodsGeneratorSettings);
                }
                else if (codeGeneratorSettings is DecoratorBaseClassesGeneratorSettings decoratorBaseClassesGeneratorSettings)
                {
                    generator = new DecoratorBaseClassesGenerator();
                    generator.NonGenericInitialize(decoratorBaseClassesGeneratorSettings);
                }
                else if (codeGeneratorSettings is SubclassCombinationImplementationsGeneratorSettings
                    subclassCombinationImplementationsGeneratorSettings)
                {
                    generator = new SubclassCombinationImplementationsGenerator();
                    generator.NonGenericInitialize(subclassCombinationImplementationsGeneratorSettings);
                }
                else if (codeGeneratorSettings is DependencyInjectableExtensionMethodsGeneratorSettings
                    dependencyInjectableExtensionMethodsGeneratorSettings)
                {
                    generator = new DependencyInjectableExtensionMethodsGenerator();
                    generator.NonGenericInitialize(dependencyInjectableExtensionMethodsGeneratorSettings);
                }
                else
                {
                    throw new ArgumentException($"Unknown settings type: {codeGeneratorSettings?.GetType()?.Name}");
                }

                var outputFiles = generator.Generate(codeIndexerService);
                foreach (var outputFile in outputFiles)
                {
                    addSource(outputFile.Key, outputFile.Value);
                    compilation =
                        compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(outputFile.Value, Encoding.UTF8),
                            options));
                }
            }
        }
    }
}