using System;
using System.Linq;
using ReactiveProcesses;

namespace MoreIO.Examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var service = new IoService(new ReactiveProcessFactory());
            var test1 = service.ToPath(Environment.CurrentDirectory).Value;

            Console.WriteLine($"Monitoring {Environment.CurrentDirectory}");
            var changes = test1.ToLiveLinq();

            changes.AsObservable().Subscribe(x =>
            {
                Console.WriteLine($"{x.Type}: {string.Join(", ", x.Values.Select(b => b.ToString()))}");
            });

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}