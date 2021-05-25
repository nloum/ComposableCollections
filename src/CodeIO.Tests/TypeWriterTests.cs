using System;
using System.Collections.Immutable;
using CodeIO.LoadedTypes.Read;
using CodeIO.LoadedTypes.Write;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeIO.Tests
{
    [TestClass]
    public class TypeWriterTests
    {
        [TestMethod]
        public void ShouldBuildClassWithProperty()
        {
            var uut = new ClassWriter()
            {
                Name = "CustomClass",
                Properties = ImmutableList<PropertyWriter>.Empty.Add(new PropertyWriter()
                {
                    Name = "Value",
                    Type = typeof(string)
                }),
            }.AddConstructorThatInitializesAllProperties();

            var type = uut.Write();

            var instance = Activator.CreateInstance(type, "Hi");
            type.GetProperty("Value").GetValue(instance).Should().Be("Hi");
        }
        
        [TestMethod]
        public void ShouldBuildClassThatImplementsInterfaceWithProperty()
        {
            var typeReader = new TypeReader();
            typeReader.AddReflection();
            var iface = (ReflectionNonGenericInterface)typeReader.GetTypeFormat<Type>()[typeof(IInterface)].Value;

            var classWriter = iface
                .Implement()
                .ImplementMissingProperties()
                .ImplementMissingMethods()
                .AddConstructorThatInitializesAllProperties();
            var type = classWriter.Write();

            var instance = (IInterface)Activator.CreateInstance(type, "Hi");
            instance.Name.Should().Be("Hi");
        }
    }
}