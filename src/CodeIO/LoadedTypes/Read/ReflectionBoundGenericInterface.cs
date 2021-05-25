using System;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionBoundGenericInterface : ReflectionComplexTypeBase, IBoundGenericInterface
    {
        public ReflectionBoundGenericInterface(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
        }
    }
}