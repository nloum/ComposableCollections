using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeIO.Tests
{
    public class ClassWithProperty
    {
        public string MyProp { get; set; }
    }

    public class ClassWithRecursiveProperty
    {
        public ClassWithRecursiveProperty Parent { get; set; }
    }
    
    [TestClass]
    public class TypeReaderTests
    {
        [TestMethod]
        public void ShouldReadClassWithProperty()
        {
            var uut = new TypeReader();
            uut.Add<ClassWithProperty>();

            var type = (INonGenericClass)uut.Types[typeof(ClassWithProperty).GetTypeIdentifier()];
            type.Identifier.Name.Should().Be(nameof(ClassWithProperty));
            type.Visibility.Should().Be(Visibility.Public);
            
            type.Properties.Count.Should().Be(1);
            type.Properties[0].Name.Should().Be("MyProp");
            type.Properties[0].Visibility.Should().Be(Visibility.Public);
            type.Properties[0].Type.Identifier.Name.Should().Be("String");
        }

        [TestMethod]
        public void ShouldReadString()
        {
            var uut = new TypeReader();
            uut.Add<string>();

            var type = (INonGenericClass)uut.Types[typeof(string).GetTypeIdentifier()];
            type.Identifier.Name.Should().Be("String");
            type.Visibility.Should().Be(Visibility.Public);
            
            type.Properties.Count.Should().Be(1);
            type.Properties[0].Name.Should().Be("Length");
            type.Properties[0].Visibility.Should().Be(Visibility.Public);
            var lengthType = (Primitive)type.Properties[0].Type;
            lengthType.Type.Should().Be(PrimitiveType.Int);
        }

        [TestMethod]
        public void ShouldClassWithRecursiveProperty()
        {
            var uut = new TypeReader();
            uut.Add<ClassWithRecursiveProperty>();

            var type = (INonGenericClass)uut.Types[typeof(ClassWithRecursiveProperty).GetTypeIdentifier()];
            type.Identifier.Name.Should().Be(nameof(ClassWithRecursiveProperty));
            type.Visibility.Should().Be(Visibility.Public);
            
            type.Properties.Count.Should().Be(1);
            type.Properties[0].Name.Should().Be("Parent");
            type.Properties[0].Visibility.Should().Be(Visibility.Public);
            object.ReferenceEquals(type.Properties[0].Type, type).Should().BeTrue();
        }
    }
}