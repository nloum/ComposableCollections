using System;
using System.Linq;
using System.Reactive.Concurrency;
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

            var _folderSubscription = testsFolder.Descendants().ToLiveLinq().ObserveOn(NewThreadScheduler.Default).Where(path =>
           (path.HasExtension("pcap") || path.HasExtension("pcapng")) &&
           path.IsFile()).Subscribe(UpsertLogFileMetadata,
            (path, removalMode) => {
                DeleteLogFileMetadata(path, removalMode);
            });

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void UpsertLogFileMetadata(AbsolutePath path)
        {
            Console.WriteLine($"{path} was added");
        }

        private static void DeleteLogFileMetadata(AbsolutePath path, ReasonForRemoval removalMode)
        {
            Console.WriteLine($"{path} was removed because {removalMode}");
        }
    }
}