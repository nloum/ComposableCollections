using System;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionBoundGenericClass : ReflectionClassBase, IBoundGenericClass
    {
        public ReflectionBoundGenericClass(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
        }
    }
}