using System;
using System.Collections.Generic;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionBoundGenericInterface : ReflectionComplexTypeBase, IBoundGenericInterface
    {
        private Lazy<IType> _unbound;
        
        public IUnboundGenericInterface Unbound => (IUnboundGenericInterface)_unbound.Value;
        public IReadOnlyList<IType> Arguments { get; }

        public ReflectionBoundGenericInterface(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
            _unbound = typeFormat[type.GetGenericTypeDefinition()];
            Arguments = type.GetGenericArguments().Select(x => typeFormat[x]).Select(x => x.Value);
        }
    }
}