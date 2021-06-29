using System;

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

    public struct TestStruct
    {
        public int _field1;
        public string _field2;
    }

    public enum TestEnum : ulong
    {
        A=1,
        B=5,
    }
    
    public class ClassWithIndexer
    {
        public string this[string a]
        {
            get => "";
            set {
            
            }
        }
    }
    
    public class ClassWithMethod
    {
        public string GetSomething(int a)
        {
            return "";
        }
    }

    public interface IInterfaceWithProperty
    {
        public string Name { get; set; }
    }
    
    public class ImplementationWithProperty : IInterfaceWithProperty
    {
        public string Name { get; set; }
    }

    public interface IInterfaceWithMethod
    {
        public string Hello();
    }
    
    public class ImplementationWithMethod : IInterfaceWithMethod
    {
        public string Hello() => "World";
    }

    public interface IInterfaceWithProperty<T>
    {
        public T Value { get; set; }
    }

    public class ImplementationWithProperty<T> : IInterfaceWithProperty<T>
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

    public interface IGenericInterface<out T> where T : IDisposable
    {
        T Value { get; }
    }
    
    public class NestingParentGeneric<T> where T : class, new()
    {
        public T Value { get; set; }
        
        public class PublicNestingChildNonGeneric
        {
            public T Value { get; init; }
        }

        public interface IPublicNestingChildNonGeneric
        {
            public T Value { get; init; }
        }
    }
}