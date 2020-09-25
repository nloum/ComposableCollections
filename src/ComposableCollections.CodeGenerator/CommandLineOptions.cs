using CommandLine;

namespace ComposableCollections.CodeGenerator
{
    public class CommandLineOptions
    {
        public CommandLineOptions(string configurationFilePath, string sourceCodePath)
        {
            ConfigurationFilePath = configurationFilePath;
            SourceCodePath = sourceCodePath;
        }

        [Option('c', "configuration")]
        public string ConfigurationFilePath { get; }
        [Option('s', "source")]
        public string SourceCodePath { get; }
    }
}