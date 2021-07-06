using System;
using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComposableCollections.Tests.Decorators
{
    [TestClass]
    public class ReadWriteLockDictionaryDecoratorTests
    {
        [TestMethod]
        public void ReadWriteOperationsShouldWorkWithConstructorSyntax()
        {
            var underlyingDictionary = new ComposableDictionary<int, string>();
            var readWriteLockDictionaryDecorator = new ReadWriteLockDictionaryDecorator<int, string>(underlyingDictionary);
            readWriteLockDictionaryDecorator.Add(3, "Hey");
            Console.WriteLine(readWriteLockDictionaryDecorator[3]);
        }
        
        // [TestMethod]
        // public void ReadWriteOperationsShouldWorkWithFluentSyntax()
        // {
        //     var readWriteLockDictionaryDecorator = new ComposableDictionary<int, string>()
        //         .WithReadWriteLock();
        //     readWriteLockDictionaryDecorator.Add(3, "Hey");
        //     Console.WriteLine(readWriteLockDictionaryDecorator[3]);
        // }
    }
}