using System;
using System.Linq;

namespace MoreIO.Examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var service = new IoService();
            var test1 = service.ToPath("test1").Value;

            var changes = test1.ToLiveLinq();

            changes.AsObservable().Subscribe(x =>
            {
                Console.WriteLine($"{x.Type}: {string.Join(", ", x.Items.Select(b => b.Key.Name))}");
            });

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}