using System;
using System.Linq;
using System.Text.RegularExpressions;
using LiveLinq;
using LiveLinq.Core;
using ReactiveProcesses;

namespace IoFluently.Examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var service = new IoService(new ReactiveProcessFactory());
            
            var repositoryRoot = service.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").Exists());

            var source = repositoryRoot / "packages";
            var destination = repositoryRoot / "new-packages";

            foreach (var sourceAndDestination in source.Translate(destination))
            {
                // Each sourceAndDestination represents a single source / destination pair.
                if (sourceAndDestination.Source.LastWriteTime() > sourceAndDestination.Destination.LastWriteTime())
                {
                    sourceAndDestination.Copy(overwrite: true);
                }
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}