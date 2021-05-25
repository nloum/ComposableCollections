using System;
using System.Collections.Generic;
using Autofac;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public abstract class ReflectionClassBase : ReflectionComplexTypeBase, IClass
    {
        public IReadOnlyList<IConstructor> Constructors { get; }

        public ReflectionClassBase(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
            
            Constructors = type.GetDeclaredConstructors().Select(constructor => new Constructor()
            {
                Parameters = constructor.GetParameters().Select(parameter =>
                    new LazyConstructorParameter(parameter.Name, typeFormat[parameter.ParameterType])),
                Visibility = constructor.GetVisibility()
            });
        }
    }
}