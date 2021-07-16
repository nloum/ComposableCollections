using System;
using System.Linq;
using Microsoft.VisualBasic;
using Serilog;
using Serilog.Core;

namespace IoFluently.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            
            var fileSystem = new LocalFileSystem();
            var repo = fileSystem.ParseAbsolutePath(@"C:\src\bluecats-csharp-loop-local\src")
                .ExpectFolder();
            var csprojFiles = repo.Descendants("*.csproj")
                .Select(csproj => csproj.ExpectTextFile());

            foreach (var csprojFile in csprojFiles)
            {
                var originalText = csprojFile.ReadAllText();
                var text = originalText.Replace("2.0.0-alpha0851", "2.0.0-alpha0857");
                var x = csprojFile.ExpectTextFileOrMissingPath().IsFile;
                if (!text.Equals(originalText))
                {
                    Log.Information("Updating {File}", csprojFile);
                    csprojFile.WriteAllText(text);
                }
            }

            Log.Information("Done");
        }
    }
}