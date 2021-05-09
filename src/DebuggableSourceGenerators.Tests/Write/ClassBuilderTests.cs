using System;
using DebuggableSourceGenerators.Write;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DebuggableSourceGenerators.Tests.Write
{
    [TestClass]
    public class ClassBuilderTests
    {
        [TestMethod]
        public void ShouldSupportCreatingClassWithoutInterfacesOrBaseClass()
        {
            var classBuilder = new ClassBuilder()
            {
                Name = "MyClass",
                IncludeConstructorForAllProperties = true,
                Properties =
                {
                    new Property()
                    {
                        Name = "Id",
                        Type = typeof(Guid),
                    },
                    new Property()
                    {
                        Name = "Name",
                        Type = typeof(string),
                    },
                },
                // Add a default constructor for EntityFrameworkCore
                Constructors = { new Constructor() }
            };
            
            var clazz = classBuilder.Build();

            var expectedId = Guid.NewGuid();
            var expectedName = "ARandomName";
            var instance = Activator.CreateInstance(clazz, expectedId, expectedName);
            var id = (Guid)clazz.GetProperty("Id").GetValue(instance);
            id.Should().Be(expectedId);
            var name = (string) clazz.GetProperty("Name").GetValue(instance);
            name.Should().Be(expectedName);
        }
        
        [TestMethod]
        public void ShouldSupportCreatingClassWithAStaticallyImplementedMethodFromInterface()
        {
            var classBuilder = new ClassBuilder()
            {
                Name = "MyClass",
                IncludeConstructorForAllProperties = true,
                Interfaces = { typeof(IHasMethod) },
                StaticMethodImplementationSources = { typeof(HasMethodImpl) },
                Constructors = { new Constructor() }
            };

            var clazz = classBuilder.Build();

            var instance = (IHasMethod)Activator.CreateInstance(clazz);
            instance.Hello().Should().Be("World!");
        }

        public interface IHasMethod
        {
            string Hello();
        }

        public static class HasMethodImpl
        {
            public static string Hello()
            {
                return "World!";
            }
        }

        [TestMethod]
        public void ShouldSupportInterfaceAndAbstractBaseClass()
        {
            var classBuilder = new ClassBuilder()
            {
                Name = "MyClass",
                IncludeConstructorForAllProperties = true,
                BaseClass = typeof(Driver),
                Interfaces = { typeof(IBoyScout) },
                StaticMethodImplementationSources = { typeof(BoyScoutImpl) },
                Properties =
                {
                    new Property()
                    {
                        Name = "Rank",
                        Type = typeof(string),
                    },
                    new Property()
                    {
                        Name = "Name",
                        Type = typeof(string),
                    },
                    new Property()
                    {
                        Name = "DriversLicenseNumber",
                        Type = typeof(string),
                    },
                },
                // Add a default constructor for EntityFrameworkCore
                Constructors = { new Constructor() }
            };
            
            var clazz = classBuilder.Build();
        }
        
        public interface IBoyScout : IPerson
        {
            public string Rank { get; set; }
        }
    
        public interface IPerson
        {
            public string Name { get; set; }
            public string FullName();
        }

        public abstract class Driver : IPerson
        {
            public abstract string Name { get; set; }
            public abstract string FullName();
            public string DriversLicenseNumber { get; }
        }
    
        public static class BoyScoutImpl {
            public static string FullName(IBoyScout boyScout)
            {
                return boyScout.Rank;
            }
        }
    }
}