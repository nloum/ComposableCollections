using System;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionUnboundGenericInterface : ReflectionComplexTypeBase, IUnboundGenericInterface
    {
        public ReflectionUnboundGenericInterface(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
        }
    }
}