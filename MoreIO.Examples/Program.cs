using System;
using SimpleMonads;

namespace MoreIO.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioService = new IoService();

            var test1 = ioService.ToPath("test1").Value;

            test1.CreateFolder()
                .ClearFolder();

            var text1 = test1.Descendant("test.txt").Value;
            
            text1.WriteText("testing 1 2 3");

            var test2 = ioService.ToPath("test2").Value
                .CreateFolder()
                .ClearFolder();

            var text2 = text1.Translate(test1, test2).Move();
            
            Console.WriteLine("Does the original file exist? " + text1.Exists());
            Console.WriteLine("Does the new file exist? " + text2.Destination.Exists());
        }
    }
}