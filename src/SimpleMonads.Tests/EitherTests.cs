using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleMonads.Tests
{
    [TestClass]
    public class EitherTests
    {
        [TestMethod]
        public void ToStringShouldReturnValidResult()
        {
            var uut = new Either<string, int>("Hi there");
            uut.ToString().Should().Be("Either<Object, string, int>(string Item1: Hi there)");
        }
        
        [TestMethod]
        public void SubTypesOfShouldAcceptClassHierarchy()
        {
            SubTypesOf<Animal>.Either<Dog, Cat> subTypesOf = new Dog();
            var isDog = false;
            var isCat = false;
            subTypesOf.ForEach(dog => isDog = true, cat => isCat = true );
            isDog.Should().BeTrue();
            isCat.Should().BeFalse();
        }

        public class Animal
        {
            
        }

        public class Dog : Animal
        {
            
        }

        public class Cat : Animal
        {
            
        }
    }
}