using System;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionBoundGenericClass : ReflectionClassBase, IBoundGenericClass
    {
        private Lazy<IType> _unbound;
        
        public IUnboundGenericClass Unbound => (IUnboundGenericClass)_unbound.Value;

        public ReflectionBoundGenericClass(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
            _unbound = typeFormat[type.GetGenericTypeDefinition()];
        }
    }
}