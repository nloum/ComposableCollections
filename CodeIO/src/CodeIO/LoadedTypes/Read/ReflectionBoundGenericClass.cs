using System;
using System.Collections.Generic;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionBoundGenericClass : ReflectionClassBase, IBoundGenericClass
    {
        private Lazy<IType> _unbound;
        
        public IUnboundGenericClass Unbound => (IUnboundGenericClass)_unbound.Value;

        public IReadOnlyList<IType> Arguments { get; }

        public ReflectionBoundGenericClass(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
            _unbound = typeFormat[type.GetGenericTypeDefinition()];
            Arguments = type.GetGenericArguments().Select(x => typeFormat[x]).Select(x => x.Value);
        }
    }
}