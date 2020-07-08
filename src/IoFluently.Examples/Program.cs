using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Xml;
using LiveLinq;
using LiveLinq.Core;
using ReactiveProcesses;

namespace IoFluently.Examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ioService = new IoService(new ReactiveProcessFactory());

            var repositoryRootFolder = ioService.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").Exists());

            ioService.CurrentDirectory.Children().Where(x => x.HasExtension(".md") && x.GetPathType() == PathType.File);


            var source = repositoryRootFolder / "packages";
            var destination = repositoryRootFolder / "new-packages";

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