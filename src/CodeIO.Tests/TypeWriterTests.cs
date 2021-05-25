using System;
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
                Properties =
                {
                    new PropertyWriter()
                    {
                        Name = "Value",
                        Type = typeof(string)
                    }
                },
                IncludeConstructorForAllProperties = true,
            };

            var type = uut.Write();

            var instance = Activator.CreateInstance(type, "Hi");
            type.GetProperty("Value").GetValue(instance).Should().Be("Hi");
        }
    }
}