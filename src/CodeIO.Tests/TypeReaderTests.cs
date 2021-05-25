using System;
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

    public interface IInterface
    {
        public string Name { get; set; }
    }
    
    public class Implementation : IInterface
    {
        public string Name { get; set; }
    }

    public interface IInterface<T>
    {
        public T Value { get; set; }
    }

    public class Implementation<T> : IInterface<T>
    {
        public T Value { get; set; }
    }

    public interface IInterfaceTMustBeReference<T> where T : class
    {
    }

    public interface IInterfaceTMustHaveDefaultConstructor<T> where T : new()
    {
    }

    public class HasPublicConstructor
    {
        public HasPublicConstructor(string name)
        {
            
        }
    }
    
    public class HasProtectedConstructor
    {
        protected HasProtectedConstructor(string name)
        {
            
        }
    }
    
    public class HasPrivateConstructor
    {
        private HasPrivateConstructor(string name)
        {
            
        }
    }
    
    public class HasInternalConstructor
    {
        internal HasInternalConstructor(string name)
        {
            
        }
    }
    
    [TestClass]
    public class TypeReaderTests
    {
        [TestMethod]
        public void ShouldReadClassWithProperty()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var type = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(ClassWithProperty)].Value;

            type.Identifier.Name.Should().Be(nameof(ClassWithProperty));
            type.Visibility.Should().Be(Visibility.Public);
            
            type.Properties.Count.Should().Be(1);
            type.Properties[0].Name.Should().Be("MyProp");
            type.Properties[0].Visibility.Should().Be(Visibility.Public);
            type.Properties[0].Type.Identifier.Name.Should().Be("String");
        }

        [TestMethod]
        public void ShouldReadInterfaceImplementation()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var type = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(Implementation)].Value;

            type.Identifier.Name.Should().Be(nameof(Implementation));
            type.Visibility.Should().Be(Visibility.Public);
            
            type.Properties.Count.Should().Be(1);
            type.Properties[0].Name.Should().Be("Name");
            type.Properties[0].Visibility.Should().Be(Visibility.Public);
            type.Properties[0].Type.Identifier.Name.Should().Be("String");

            var iface = (INonGenericInterface) uut.GetTypeFormat<Type>()[typeof(IInterface)].Value;
            iface.Visibility.Should().Be(Visibility.Public);
            
            iface.Properties.Count.Should().Be(1);
            iface.Properties[0].Name.Should().Be("Name");
            iface.Properties[0].Visibility.Should().Be(Visibility.Public);
            iface.Properties[0].Type.Identifier.Name.Should().Be("String");
        }

        [TestMethod]
        public void ShouldReadGenericParameterThatMustHaveDefaultConstructor()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var type = (IUnboundGenericInterface)uut.GetTypeFormat<Type>()[typeof(IInterfaceTMustHaveDefaultConstructor<>)].Value;

            type.Identifier.Name.Should().Be("IInterfaceTMustHaveDefaultConstructor");
            type.Visibility.Should().Be(Visibility.Public);
        }
        
        [TestMethod]
        public void ShouldReadTypeWithPublicConstructor()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var type = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(HasPublicConstructor)].Value;

            type.Identifier.Name.Should().Be(nameof(HasPublicConstructor));
            type.Visibility.Should().Be(Visibility.Public);
            type.Constructors.Count.Should().Be(1);
            type.Constructors[0].Visibility.Should().Be(Visibility.Public);
            type.Constructors[0].Parameters.Count.Should().Be(1);
            type.Constructors[0].Parameters[0].Name.Should().Be("name");
            type.Constructors[0].Parameters[0].Type.Identifier.Name.Should().Be("String");
        }
        
        [TestMethod]
        public void ShouldReadTypeWithPrivateConstructor()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var type = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(HasPrivateConstructor)].Value;

            type.Identifier.Name.Should().Be(nameof(HasPrivateConstructor));
            type.Visibility.Should().Be(Visibility.Public);
            type.Constructors.Count.Should().Be(1);
            type.Constructors[0].Visibility.Should().Be(Visibility.Private);
            type.Constructors[0].Parameters.Count.Should().Be(1);
            type.Constructors[0].Parameters[0].Name.Should().Be("name");
            type.Constructors[0].Parameters[0].Type.Identifier.Name.Should().Be("String");
        }
        
        [TestMethod]
        public void ShouldReadTypeWithProtectedConstructor()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var type = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(HasProtectedConstructor)].Value;

            type.Identifier.Name.Should().Be(nameof(HasProtectedConstructor));
            type.Visibility.Should().Be(Visibility.Public);
            type.Constructors.Count.Should().Be(1);
            type.Constructors[0].Visibility.Should().Be(Visibility.Protected);
            type.Constructors[0].Parameters.Count.Should().Be(1);
            type.Constructors[0].Parameters[0].Name.Should().Be("name");
            type.Constructors[0].Parameters[0].Type.Identifier.Name.Should().Be("String");
        }
        
        [TestMethod]
        public void ShouldReadTypeWithInternalConstructor()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var type = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(HasInternalConstructor)].Value;

            type.Identifier.Name.Should().Be(nameof(HasInternalConstructor));
            type.Visibility.Should().Be(Visibility.Public);
            type.Constructors.Count.Should().Be(1);
            type.Constructors[0].Visibility.Should().Be(Visibility.Internal);
            type.Constructors[0].Parameters.Count.Should().Be(1);
            type.Constructors[0].Parameters[0].Name.Should().Be("name");
            type.Constructors[0].Parameters[0].Type.Identifier.Name.Should().Be("String");
        }

        [TestMethod]
        public void ShouldReadUnboundGenericInterfaceImplementation()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var type = (IUnboundGenericClass)uut.GetTypeFormat<Type>()[typeof(Implementation<>)].Value;

            type.Identifier.Name.Should().Be(nameof(Implementation));
            type.Visibility.Should().Be(Visibility.Public);
            
            type.Properties.Count.Should().Be(1);
            type.Properties[0].Name.Should().Be("Value");
            type.Properties[0].Visibility.Should().Be(Visibility.Public);
            type.Properties[0].Type.Identifier.Name.Should().Be("T");

            var iface = (IUnboundGenericInterface) uut.GetTypeFormat<Type>()[typeof(IInterface<>)].Value;
            iface.Visibility.Should().Be(Visibility.Public);
            
            iface.Properties.Count.Should().Be(1);
            iface.Properties[0].Name.Should().Be("Value");
            iface.Properties[0].Visibility.Should().Be(Visibility.Public);
            iface.Properties[0].Type.Identifier.Name.Should().Be("T");
        }

        [TestMethod]
        public void ShouldReadBoundGenericInterfaceImplementation()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var type = (IBoundGenericClass)uut.GetTypeFormat<Type>()[typeof(Implementation<string>)].Value;

            type.Identifier.Name.Should().Be(nameof(Implementation));
            type.Visibility.Should().Be(Visibility.Public);
            
            type.Properties.Count.Should().Be(1);
            type.Properties[0].Name.Should().Be("Value");
            type.Properties[0].Visibility.Should().Be(Visibility.Public);
            type.Properties[0].Type.Identifier.Name.Should().Be("String");

            var iface = (IBoundGenericInterface) uut.GetTypeFormat<Type>()[typeof(IInterface<string>)].Value;
            iface.Visibility.Should().Be(Visibility.Public);
            
            iface.Properties.Count.Should().Be(1);
            iface.Properties[0].Name.Should().Be("Value");
            iface.Properties[0].Visibility.Should().Be(Visibility.Public);
            iface.Properties[0].Type.Identifier.Name.Should().Be("String");
        }

        [TestMethod]
        public void ShouldReadString()
        {
            var uut = new TypeReader();
            uut.AddReflection();

            var type = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(string)].Value;
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
            uut.AddReflection();

            var type = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(ClassWithRecursiveProperty)].Value;
            type.Identifier.Name.Should().Be(nameof(ClassWithRecursiveProperty));
            type.Visibility.Should().Be(Visibility.Public);
            
            type.Properties.Count.Should().Be(1);
            type.Properties[0].Name.Should().Be("Parent");
            type.Properties[0].Visibility.Should().Be(Visibility.Public);
            object.ReferenceEquals(type.Properties[0].Type, type).Should().BeTrue();
        }
    }
}