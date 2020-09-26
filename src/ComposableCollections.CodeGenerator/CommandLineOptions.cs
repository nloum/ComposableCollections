using System.Drawing;
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

        [Value(0, Required = true)]
        public string ConfigurationFilePath { get; }
        [Option('s', "source", Required = false)]
        public string SourceCodePath { get; }
    }
}