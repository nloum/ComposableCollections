using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Autofac;
using IoFluently;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace ComposableCollections.CodeGenerator
{
    public class CommandLineService : ICommandLineService
    {
        private readonly IIoService _ioService;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IPathService _pathService;

        public CommandLineService(IIoService ioService, ILifetimeScope lifetimeScope, IPathService pathService)
        {
            _ioService = ioService;
            _lifetimeScope = lifetimeScope;
            _pathService = pathService;
        }

        public void Run(CommandLineOptions options)
        {
            var configurationFilePath = _ioService.ParseAbsolutePath(options.ConfigurationFilePath, _ioService.CurrentDirectory);
            var sourceCodePath = string.IsNullOrWhiteSpace(options.SourceCodePath)
                ? configurationFilePath.Parent()
                : _ioService.ParseAbsolutePath(options.SourceCodePath, _ioService.CurrentDirectory);
            _pathService.Initialize(sourceCodePath);

            var configurationFile = configurationFilePath.AsSerializedXmlFile<Configuration>();
            var configuration = configurationFile.Read();

            var csGeneratedFiles = sourceCodePath.Descendants()
                .Where(child => child.Name.EndsWith(".g.cs"));

            // foreach (var csGeneratedFile in csGeneratedFiles)
            // {
            //     csGeneratedFile.DeleteFile();
            // }

            var csFiles = sourceCodePath.Descendants()
                .Where(child => child.HasExtension(".cs"));
			
            var syntaxTrees = csFiles.Select(csFile =>
                CSharpSyntaxTree.ParseText(SourceText.From(csFile.ReadText(), Encoding.UTF8))).ToImmutableList();

            var dllFiles = sourceCodePath.Descendants()
                .Where(child => child.HasExtension(".dll"))
                .OrderBy(file => file)
                .GroupBy(file => file.Name)
                .Select(group => group.First())
                .ToImmutableList();

            var compilation = CSharpCompilation.Create("HelloWorld");

            foreach (var dllFile in dllFiles)
            {
                compilation = compilation
                    .AddReferences(MetadataReference.CreateFromFile(typeof(string).Assembly.Location),
                        MetadataReference.CreateFromFile(dllFile.ToString()));
            }
            
            compilation = compilation.AddSyntaxTrees(syntaxTrees);
            
            foreach (var codeGeneratorSettings in configuration.CodeGenerators)
            {
                var generator = _lifetimeScope.ResolveKeyed<GeneratorBase>(codeGeneratorSettings.GetType());
                generator.NonGenericInitialize(codeGeneratorSettings);
                var outputFiles = generator.Generate(syntaxTrees, tree => compilation.GetSemanticModel(tree));
                foreach (var outputFile in outputFiles)
                {
                    if (!outputFile.Key.Exists())
                    {
                        int a = 3;
                    }
                    _ioService.WriteText(outputFile.Key, outputFile.Value);
                }
            }
        }
    }
}