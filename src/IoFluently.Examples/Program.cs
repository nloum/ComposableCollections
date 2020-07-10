using System;
using System.Linq;
using LiveLinq;
using ReactiveProcesses;

namespace IoFluently.Examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ioService = new IoService(new ReactiveProcessFactory());

            var testsFolder = ioService.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").Exists()) / "tests";

            testsFolder.Descendants().ToLiveLinq().Subscribe(path => Console.WriteLine($"{path} has been added"), (path, removalMode) =>
            {
                if (removalMode.Item4.HasValue)
                {
                    var x = removalMode.Item4.Value;
                    Console.WriteLine(x.Exception);
                }
                Console.WriteLine($"{path} has been removed because {removalMode}");
            });

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}