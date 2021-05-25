using System;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionNonGenericInterface : ReflectionComplexTypeBase, INonGenericInterface
    {
        public ReflectionNonGenericInterface(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
        }
    }
}