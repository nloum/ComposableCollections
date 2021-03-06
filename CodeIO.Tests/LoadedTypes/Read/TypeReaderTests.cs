using System;
using System.Linq;
using CodeIO.LoadedTypes.Read;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeIO.Tests
{
    [TestClass]
    public class TypeReaderTests
    {
        [TestMethod]
        public void ShouldReadClassWithProperty()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(ClassWithProperty)].Value;

            clazz.Identifier.Name.Should().Be(nameof(ClassWithProperty));
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("MyProp");
            clazz.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            clazz.Properties[0].Type.Identifier.Name.Should().Be("String");
        }
        
        [TestMethod]
        public void ShouldReadStructureWithFields()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
            var type = (IStructure)uut.GetTypeFormat<Type>()[typeof(TestStruct)].Value;

            type.Identifier.Name.Should().Be(nameof(TestStruct));
            type.Visibility.Should().Be(Visibility.Public);
            
            type.Fields.Count.Should().Be(2);
            type.Fields[0].Name.Should().Be("_field1");
            type.Fields[0].Visibility.Should().Be(Visibility.Public);
            type.Fields[0].Type.Identifier.Name.Should().Be("Int32");
            
            type.Fields[1].Name.Should().Be("_field2");
            type.Fields[1].Visibility.Should().Be(Visibility.Public);
            type.Fields[1].Type.Identifier.Name.Should().Be("String");
        }

        [TestMethod]
        public void ShouldReadEnum()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
            var type = (IEnum)uut.GetTypeFormat<Type>()[typeof(TestEnum)].Value;

            type.Identifier.Name.Should().Be(nameof(TestEnum));
            type.Visibility.Should().Be(Visibility.Public);

            type.Values.Count.Should().Be(2);
            type.Values[0].Name.Should().Be("A");
            type.Values[0].Value.Should().Be((ulong)1);
            type.Values[1].Name.Should().Be("B");
            type.Values[1].Value.Should().Be((ulong)5);
        }

        [TestMethod]
        public void ShouldReadInterfaceImplementation()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(ImplementationWithProperty)].Value;

            clazz.Identifier.Name.Should().Be(nameof(ImplementationWithProperty));
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Name");
            clazz.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            clazz.Properties[0].Type.Identifier.Name.Should().Be("String");

            var iface = (INonGenericInterface) uut.GetTypeFormat<Type>()[typeof(IInterfaceWithProperty)].Value;
            object.ReferenceEquals(iface, clazz.Interfaces[0]).Should().BeTrue();
            iface.Visibility.Should().Be(Visibility.Public);
            
            iface.Properties.Count.Should().Be(1);
            iface.Properties[0].Name.Should().Be("Name");
            iface.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            iface.Properties[0].Type.Identifier.Name.Should().Be("String");
        }

        [TestMethod]
        public void ShouldReadGenericParameterThatMustHaveDefaultConstructor()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
            var iface = (IUnboundGenericInterface)uut.GetTypeFormat<Type>()[typeof(IInterfaceTMustHaveDefaultConstructor<>)].Value;

            iface.Identifier.Name.Should().Be("IInterfaceTMustHaveDefaultConstructor");
            iface.Visibility.Should().Be(Visibility.Public);
        }
        
        [TestMethod]
        public void ShouldReadUnboundGenericClass()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
            var clazz = (IUnboundGenericClass)uut.GetTypeFormat<Type>()[typeof(NestingParentGeneric<>)].Value;

            clazz.Identifier.Name.Should().Be("NestingParentGeneric");
            clazz.Visibility.Should().Be(Visibility.Public);
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Value");
            clazz.Properties[0].Type.Identifier.Name.Should().Be("T");

            clazz.Parameters.Count.Should().Be(1);
            clazz.Parameters[0].MustBeReferenceType.Should().BeTrue();
            clazz.Parameters[0].MustHaveDefaultConstructor.Should().BeTrue();
            clazz.Parameters[0].IsCovariant.Should().BeFalse();
            clazz.Parameters[0].IsContravariant.Should().BeFalse();
            clazz.Parameters[0].MustBeNonNullable.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldReadBoundGenericClass()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
            var clazz = (IBoundGenericClass)uut.GetTypeFormat<Type>()[typeof(NestingParentGeneric<ImplementationWithMethod>)].Value;

            clazz.Identifier.Name.Should().Be("NestingParentGeneric");
            clazz.Visibility.Should().Be(Visibility.Public);
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Value");
            ((IReflectionType)clazz.Properties[0].Type).Type.Should().Be(typeof(ImplementationWithMethod));

            var unbound = clazz.Unbound;
            
            unbound.Parameters.Count.Should().Be(1);
            unbound.Parameters[0].MustBeReferenceType.Should().BeTrue();
            unbound.Parameters[0].MustHaveDefaultConstructor.Should().BeTrue();
            unbound.Parameters[0].IsCovariant.Should().BeFalse();
            unbound.Parameters[0].IsContravariant.Should().BeFalse();
            unbound.Parameters[0].MustBeNonNullable.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldReadUnboundGenericInterface()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
            var clazz = (IUnboundGenericInterface)uut.GetTypeFormat<Type>()[typeof(IGenericInterface<>)].Value;

            clazz.Identifier.Name.Should().Be("IGenericInterface");
            clazz.Visibility.Should().Be(Visibility.Public);
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Value");
            clazz.Properties[0].Type.Identifier.Name.Should().Be("T");

            clazz.Parameters.Count.Should().Be(1);
            clazz.Parameters[0].MustBeReferenceType.Should().BeFalse();
            clazz.Parameters[0].MustHaveDefaultConstructor.Should().BeFalse();
            clazz.Parameters[0].IsCovariant.Should().BeTrue();
            clazz.Parameters[0].IsContravariant.Should().BeFalse();
            clazz.Parameters[0].MustBeNonNullable.Should().BeFalse();
        }

        [Ignore] // Ignore this because nested types aren't supported yet
        [TestMethod]
        public void ShouldReadNestedClassUnderGeneric()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(NestingParentGeneric<>.PublicNestingChildNonGeneric)].Value;

            clazz.Identifier.Name.Should().Be("NestingParentNonGeneric");
            clazz.Visibility.Should().Be(Visibility.Public);
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Value");
            clazz.Properties[0].Type.Should().Be("T");
        }

        [Ignore] // Ignore this because nested types aren't supported yet
        [TestMethod]
        public void ShouldReadNestedInterfaceUnderGeneric()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
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
            uut.AddSupportForReflection();
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
            uut.AddSupportForReflection();
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
            uut.AddSupportForReflection();
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
            uut.AddSupportForReflection();
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
            uut.AddSupportForReflection();
            var clazz = (IUnboundGenericClass)uut.GetTypeFormat<Type>()[typeof(ImplementationWithProperty<>)].Value;

            clazz.Identifier.Name.Should().Be(nameof(ImplementationWithProperty));
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Value");
            clazz.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            clazz.Properties[0].Type.Identifier.Name.Should().Be("T");

            var iface = (IUnboundGenericInterface) uut.GetTypeFormat<Type>()[typeof(IInterfaceWithProperty<>)].Value;
            object.ReferenceEquals(iface, clazz.Interfaces[0]).Should().BeFalse();
            iface.Visibility.Should().Be(Visibility.Public);
            
            iface.Properties.Count.Should().Be(1);
            iface.Properties[0].Name.Should().Be("Value");
            iface.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            iface.Properties[0].Type.Identifier.Name.Should().Be("T");
        }

        [TestMethod]
        public void ShouldReadBoundGenericInterfaceImplementation()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();
            var clazz = (IBoundGenericClass)uut.GetTypeFormat<Type>()[typeof(ImplementationWithProperty<string>)].Value;

            clazz.Identifier.Name.Should().Be(nameof(ImplementationWithProperty));
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Value");
            clazz.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            clazz.Properties[0].Type.Identifier.Name.Should().Be("String");
            
            clazz.Unbound.Identifier.Name.Should().Be(nameof(ImplementationWithProperty));
            clazz.Unbound.Visibility.Should().Be(Visibility.Public);
            
            clazz.Unbound.Properties.Count.Should().Be(1);
            clazz.Unbound.Properties[0].Name.Should().Be("Value");
            clazz.Unbound.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            clazz.Unbound.Properties[0].Type.Identifier.Name.Should().Be("T");

            var iface = (IBoundGenericInterface) uut.GetTypeFormat<Type>()[typeof(IInterfaceWithProperty<string>)].Value;
            object.ReferenceEquals(iface, clazz.Interfaces[0]).Should().BeTrue();
            iface.Visibility.Should().Be(Visibility.Public);
            
            iface.Properties.Count.Should().Be(1);
            iface.Properties[0].Name.Should().Be("Value");
            iface.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            iface.Properties[0].Type.Identifier.Name.Should().Be("String");
            
            iface.Unbound.Identifier.Name.Should().Be(nameof(IInterfaceWithProperty));
            iface.Unbound.Visibility.Should().Be(Visibility.Public);
            
            iface.Unbound.Properties.Count.Should().Be(1);
            iface.Unbound.Properties[0].Name.Should().Be("Value");
            iface.Unbound.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            iface.Unbound.Properties[0].Type.Identifier.Name.Should().Be("T");
        }

        [TestMethod]
        public void ShouldReadString()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();

            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(string)].Value;
            clazz.Identifier.Name.Should().Be("String");
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Length");
            clazz.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            var lengthType = (Primitive)clazz.Properties[0].Type;
            lengthType.PrimitiveType.Should().Be(PrimitiveType.Int);
        }

        [TestMethod]
        public void ShouldReadClassWithRecursiveProperty()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();

            var clazz = (INonGenericClass)uut.GetTypeFormat<Type>()[typeof(ClassWithRecursiveProperty)].Value;
            clazz.Identifier.Name.Should().Be(nameof(ClassWithRecursiveProperty));
            clazz.Visibility.Should().Be(Visibility.Public);
            
            clazz.Properties.Count.Should().Be(1);
            clazz.Properties[0].Name.Should().Be("Parent");
            clazz.Properties[0].GetterVisibility.Should().Be(Visibility.Public);
            object.ReferenceEquals(clazz.Properties[0].Type, clazz).Should().BeTrue();
        }

        [TestMethod]
        public void ShouldReadClassWithIndexer()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();

            var clazz = (INonGenericClass) uut.GetTypeFormat<Type>()[typeof(ClassWithIndexer)].Value;
            clazz.Identifier.Name.Should().Be(nameof(ClassWithIndexer));
            clazz.Visibility.Should().Be(Visibility.Public);
            var strin = (INonGenericClass) uut.GetTypeFormat<Type>()[typeof(string)].Value;

            clazz.Indexers.Count.Should().Be(1);
            clazz.Indexers[0].GetterVisibility.Should().Be(Visibility.Public);
            clazz.Indexers[0].Parameters.Count.Should().Be(1);
            clazz.Indexers[0].Parameters[0].Name.Should().Be("a");
            object.ReferenceEquals(clazz.Indexers[0].Parameters[0].Type, strin).Should().BeTrue();
        }

        [TestMethod]
        public void ShouldReadClassWithMethod()
        {
            var uut = new TypeReader();
            uut.AddSupportForReflection();

            var clazz = (INonGenericClass) uut.GetTypeFormat<Type>()[typeof(ClassWithMethod)].Value;
            clazz.Identifier.Name.Should().Be(nameof(ClassWithMethod));
            clazz.Visibility.Should().Be(Visibility.Public);
            var inttype = (Primitive) uut.GetTypeFormat<Type>()[typeof(int)].Value;

            var method = clazz.Methods.First(method => method.Name == "GetSomething");
            method.Visibility.Should().Be(Visibility.Public);
            method.Name.Should().Be("GetSomething");
            method.Parameters.Count.Should().Be(1);
            method.Parameters[0].Name.Should().Be("a");
            object.ReferenceEquals(method.Parameters[0].Type, inttype).Should().BeTrue();
        }
    }
}