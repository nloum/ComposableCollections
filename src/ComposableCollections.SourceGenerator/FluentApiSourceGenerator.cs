using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ComposableCollections.SourceGenerator
{
    [Generator]
    public class FluentApiSourceGenerator : ISourceGenerator
    {
        public void Execute(SourceGeneratorContext context)
        {
            var configFile = context.AdditionalFiles.SingleOrDefault(additionalFile => Path.GetFileName(additionalFile.Path) == "FluentApiSourceGenerator.xml");

            if (configFile == null)
            {
                return;
            }

            var serializer = new XmlSerializer(typeof(FluentApiSourceGeneratorConfig));

            FluentApiSourceGeneratorConfig config;

            using(var streamReader = new StreamReader(configFile.Path))
            {
                config = (FluentApiSourceGeneratorConfig)serializer.Deserialize(streamReader);
            }

            foreach (var syntaxTree in context.Compilation.SyntaxTrees)
            {
                Traverse(syntaxTree.GetRoot(), syntaxNode =>
                {
                    foreach(var fluentApiSourceGenerator in config.SourceGenerators)
                    {
                        fluentApiSourceGenerator.AddSyntaxNode(syntaxNode);
                    }
                });
            }

            foreach (var fluentApiSourceGenerator in config.SourceGenerators)
            {
                fluentApiSourceGenerator.AddSource((fileName, sourceText) => context.AddSource(fileName, sourceText));
            }
        }

        private void Traverse(SyntaxNode syntaxNode, Action<SyntaxNode> action)
        {
            action(syntaxNode);

            foreach(var child in syntaxNode.ChildNodes())
            {
                Traverse(child, action);
            }
        }

        public void Initialize(InitializationContext context)
        {
            // No initialization required for this one
        }
    }
}