using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Autofac;
using IoFluently;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace ComposableCollections.CodeGenerator
{
    public class CommandLineService : ICommandLineService
    {
        private readonly IIoService _ioService;
        private readonly ILifetimeScope _lifetimeScope;

        public CommandLineService(IIoService ioService, ILifetimeScope lifetimeScope)
        {
            _ioService = ioService;
            _lifetimeScope = lifetimeScope;
        }

        public void Run(CommandLineOptions options)
        {
            var configurationFilePath = _ioService.ParseAbsolutePath(options.ConfigurationFilePath, _ioService.CurrentDirectory);
            var sourceCodePath = string.IsNullOrWhiteSpace(options.SourceCodePath)
                ? configurationFilePath.Parent()
                : _ioService.ParseAbsolutePath(options.SourceCodePath, _ioService.CurrentDirectory);

            var configurationFile = configurationFilePath.AsSerializedXmlFile<Configuration>();
            var configuration = configurationFile.Read();

            var csFiles = sourceCodePath.Descendants()
                .Where(child => IoExtensions.HasExtension(child, ".cs"));
			
            var syntaxTrees = csFiles.Select(csFile =>
                CSharpSyntaxTree.ParseText(SourceText.From(csFile.ReadText(), Encoding.UTF8))).ToImmutableList();
			
            var compilation = CSharpCompilation.Create("HelloWorld")
                // .AddReferences(MetadataReference.CreateFromFile(
                //  typeof(string).Assembly.Location),
                //  MetadataReference.CreateFromFile((repoRoot / "src" / "ComposableCollections" / "bin" / "Debug" / "netstandard2.0" / "ComposableCollections.dll").ToString()))
                .AddSyntaxTrees(syntaxTrees);
            
            foreach (var codeGeneratorSettings in configuration.CodeGenerators)
            {
                var generator = _lifetimeScope.ResolveKeyed<GeneratorBase>(codeGeneratorSettings.GetType());
                generator.NonGenericInitialize(codeGeneratorSettings);
                var outputFiles = generator.Generate(syntaxTrees, tree => compilation.GetSemanticModel(tree));
                foreach (var outputFile in outputFiles)
                {
                    _ioService.WriteText(sourceCodePath / outputFile.Key, outputFile.Value);
                }
            }
        }
    }
}