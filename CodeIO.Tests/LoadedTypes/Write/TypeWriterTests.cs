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
            }.WithConstructorThatInitializesAllProperties();

            var type = uut.Write();

            var instance = Activator.CreateInstance(type, "Hi");
            type.GetProperty("Value").GetValue(instance).Should().Be("Hi");
        }
        
        [TestMethod]
        public void ShouldBuildClassThatImplementsInterfaceWithProperty()
        {
            var typeReader = new TypeReader();
            typeReader.AddSupportForReflection();
            var iface = (ReflectionNonGenericInterface)typeReader.GetTypeFormat<Type>()[typeof(IInterfaceWithProperty)].Value;

            var classWriter = iface
                .ImplementClass()
                .WithDefaultImplementationsForMissingProperties()
                .WithConstructorThatInitializesAllProperties();
            var type = classWriter.Write();

            var instance = (IInterfaceWithProperty)Activator.CreateInstance(type, "Hi");
            instance.Name.Should().Be("Hi");
        }
        
        [TestMethod]
        public void ShouldBuildClassThatImplementsInterfaceWithMethod()
        {
            var typeReader = new TypeReader();
            typeReader.AddSupportForReflection();
            var iface = (ReflectionNonGenericInterface)typeReader.GetTypeFormat<Type>()[typeof(IInterfaceWithMethod)].Value;

            var classWriter = iface
                .ImplementClass()
                .WithStaticImplementationsForMissingMethods(typeof(TypeWriterTests));
            var type = classWriter.Write();

            var instance = (IInterfaceWithMethod)Activator.CreateInstance(type);
            instance.Hello().Should().Be("World");
        }

        public static string Hello(IInterfaceWithMethod _) => "World";
    }
}