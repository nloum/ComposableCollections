using System;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionUnboundGenericClass : ReflectionClassBase, IUnboundGenericClass
    {
        public ReflectionUnboundGenericClass(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
        }
    }
}