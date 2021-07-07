using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionNonGenericClass : ReflectionClassBase, INonGenericClass
    {
        public ReflectionNonGenericClass(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
        }
    }
}