using System;

namespace IoFluently.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSystem = new LocalFileSystem();
            var path = fileSystem.GenerateUniqueTemporaryPath()
                .ExpectTextFileOrMissingPath()
                .WriteAllText("Test 1 2 3");
            path.ExpectFile();
            
            Console.WriteLine("A happy ending");
        }
    }
}