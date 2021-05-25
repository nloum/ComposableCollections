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
}