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

    public class NestingParentGeneric<T>
    {
        public class PublicNestingChildNonGeneric
        {
            public T Value { get; init; }
        }

        public interface IPublicNestingChildNonGeneric
        {
            public T Value { get; init; }
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
            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(ClassWithProperty)].Value;

            clazz.Identifier.Name.Should().Be(nameof(ClassWithProperty));
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("MyProp");
            clazz.Properties[0].Visibility.Should().Be(Visibility.Public);
            clazz.Properties[0].Type.Identifier.Name.Should().Be("String");
        }

        [TestMethod]
        public void ShouldReadInterfaceImplementation()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(Implementation)].Value;

            clazz.Identifier.Name.Should().Be(nameof(Implementation));
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Name");
            clazz.Properties[0].Visibility.Should().Be(Visibility.Public);
            clazz.Properties[0].Type.Identifier.Name.Should().Be("String");

            var iface = (INonGenericInterface) uut.GetTypeFormat<Type>()[typeof(IInterface)].Value;
            object.ReferenceEquals(iface, clazz.Interfaces[0]).Should().BeTrue();
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
            var iface = (IUnboundGenericInterface)uut.GetTypeFormat<Type>()[typeof(IInterfaceTMustHaveDefaultConstructor<>)].Value;

            iface.Identifier.Name.Should().Be("IInterfaceTMustHaveDefaultConstructor");
            iface.Visibility.Should().Be(Visibility.Public);
        }
        
        [TestMethod]
        public void ShouldReadNestedClassUnderGeneric()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(NestingParentGeneric<>.PublicNestingChildNonGeneric)].Value;

            clazz.Identifier.Name.Should().Be("NestingParentNonGeneric");
            clazz.Visibility.Should().Be(Visibility.Public);
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Value");
            clazz.Properties[0].Type.Should().Be("T");
        }

        [TestMethod]
        public void ShouldReadNestedInterfaceUnderGeneric()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var clazz = (INonGenericInterface)uut.GetTypeFormat<Type>()[typeof(NestingParentGeneric<>.IPublicNestingChildNonGeneric)].Value;

            clazz.Identifier.Name.Should().Be("INestingParentNonGeneric");
            clazz.Visibility.Should().Be(Visibility.Public);
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Value");
            clazz.Properties[0].Type.Should().Be("T");
        }

        [TestMethod]
        public void ShouldReadTypeWithPublicConstructor()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(HasPublicConstructor)].Value;

            clazz.Identifier.Name.Should().Be(nameof(HasPublicConstructor));
            clazz.Visibility.Should().Be(Visibility.Public);
            clazz.Constructors.Count.Should().Be(1);
            clazz.Constructors[0].Visibility.Should().Be(Visibility.Public);
            clazz.Constructors[0].Parameters.Count.Should().Be(1);
            clazz.Constructors[0].Parameters[0].Name.Should().Be("name");
            clazz.Constructors[0].Parameters[0].Type.Identifier.Name.Should().Be("String");
        }
        
        [TestMethod]
        public void ShouldReadTypeWithPrivateConstructor()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(HasPrivateConstructor)].Value;

            clazz.Identifier.Name.Should().Be(nameof(HasPrivateConstructor));
            clazz.Visibility.Should().Be(Visibility.Public);
            clazz.Constructors.Count.Should().Be(1);
            clazz.Constructors[0].Visibility.Should().Be(Visibility.Private);
            clazz.Constructors[0].Parameters.Count.Should().Be(1);
            clazz.Constructors[0].Parameters[0].Name.Should().Be("name");
            clazz.Constructors[0].Parameters[0].Type.Identifier.Name.Should().Be("String");
        }
        
        [TestMethod]
        public void ShouldReadTypeWithProtectedConstructor()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(HasProtectedConstructor)].Value;

            clazz.Identifier.Name.Should().Be(nameof(HasProtectedConstructor));
            clazz.Visibility.Should().Be(Visibility.Public);
            clazz.Constructors.Count.Should().Be(1);
            clazz.Constructors[0].Visibility.Should().Be(Visibility.Protected);
            clazz.Constructors[0].Parameters.Count.Should().Be(1);
            clazz.Constructors[0].Parameters[0].Name.Should().Be("name");
            clazz.Constructors[0].Parameters[0].Type.Identifier.Name.Should().Be("String");
        }
        
        [TestMethod]
        public void ShouldReadTypeWithInternalConstructor()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(HasInternalConstructor)].Value;

            clazz.Identifier.Name.Should().Be(nameof(HasInternalConstructor));
            clazz.Visibility.Should().Be(Visibility.Public);
            clazz.Constructors.Count.Should().Be(1);
            clazz.Constructors[0].Visibility.Should().Be(Visibility.Internal);
            clazz.Constructors[0].Parameters.Count.Should().Be(1);
            clazz.Constructors[0].Parameters[0].Name.Should().Be("name");
            clazz.Constructors[0].Parameters[0].Type.Identifier.Name.Should().Be("String");
        }

        [TestMethod]
        public void ShouldReadUnboundGenericInterfaceImplementation()
        {
            var uut = new TypeReader();
            uut.AddReflection();
            var clazz = (IUnboundGenericClass)uut.GetTypeFormat<Type>()[typeof(Implementation<>)].Value;

            clazz.Identifier.Name.Should().Be(nameof(Implementation));
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Value");
            clazz.Properties[0].Visibility.Should().Be(Visibility.Public);
            clazz.Properties[0].Type.Identifier.Name.Should().Be("T");

            var iface = (IUnboundGenericInterface) uut.GetTypeFormat<Type>()[typeof(IInterface<>)].Value;
            object.ReferenceEquals(iface, clazz.Interfaces[0]).Should().BeFalse();
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
            var clazz = (IBoundGenericClass)uut.GetTypeFormat<Type>()[typeof(Implementation<string>)].Value;

            clazz.Identifier.Name.Should().Be(nameof(Implementation));
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Value");
            clazz.Properties[0].Visibility.Should().Be(Visibility.Public);
            clazz.Properties[0].Type.Identifier.Name.Should().Be("String");
            
            clazz.Unbound.Identifier.Name.Should().Be(nameof(Implementation));
            clazz.Unbound.Visibility.Should().Be(Visibility.Public);
            
            clazz.Unbound.Properties.Count.Should().Be(1);
            clazz.Unbound.Properties[0].Name.Should().Be("Value");
            clazz.Unbound.Properties[0].Visibility.Should().Be(Visibility.Public);
            clazz.Unbound.Properties[0].Type.Identifier.Name.Should().Be("T");

            var iface = (IBoundGenericInterface) uut.GetTypeFormat<Type>()[typeof(IInterface<string>)].Value;
            object.ReferenceEquals(iface, clazz.Interfaces[0]).Should().BeTrue();
            iface.Visibility.Should().Be(Visibility.Public);
            
            iface.Properties.Count.Should().Be(1);
            iface.Properties[0].Name.Should().Be("Value");
            iface.Properties[0].Visibility.Should().Be(Visibility.Public);
            iface.Properties[0].Type.Identifier.Name.Should().Be("String");
            
            iface.Unbound.Identifier.Name.Should().Be("IInterface");
            iface.Unbound.Visibility.Should().Be(Visibility.Public);
            
            iface.Unbound.Properties.Count.Should().Be(1);
            iface.Unbound.Properties[0].Name.Should().Be("Value");
            iface.Unbound.Properties[0].Visibility.Should().Be(Visibility.Public);
            iface.Unbound.Properties[0].Type.Identifier.Name.Should().Be("T");
        }

        [TestMethod]
        public void ShouldReadString()
        {
            var uut = new TypeReader();
            uut.AddReflection();

            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(string)].Value;
            clazz.Identifier.Name.Should().Be("String");
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Length");
            clazz.Properties[0].Visibility.Should().Be(Visibility.Public);
            var lengthType = (Primitive)clazz.Properties[0].Type;
            lengthType.Type.Should().Be(PrimitiveType.Int);
        }

        [TestMethod]
        public void ShouldClassWithRecursiveProperty()
        {
            var uut = new TypeReader();
            uut.AddReflection();

            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(ClassWithRecursiveProperty)].Value;
            clazz.Identifier.Name.Should().Be(nameof(ClassWithRecursiveProperty));
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Parent");
            clazz.Properties[0].Visibility.Should().Be(Visibility.Public);
            object.ReferenceEquals(clazz.Properties[0].Type, clazz).Should().BeTrue();
        }
    }
}