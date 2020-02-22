using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using LiveLinq.Core;
using LiveLinq.Dictionary;
using TreeLinq;

namespace MoreIO.Examples
{
    class Program
    {
        static void Main(string[] args)
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