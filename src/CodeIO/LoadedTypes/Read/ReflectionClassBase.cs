using System;
using System.Collections.Generic;
using Autofac;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public abstract class ReflectionClassBase : ReflectionComplexTypeBase, IClass
    {
        private readonly Lazy<IType> _baseClass;
        
        public IReadOnlyList<IConstructor> Constructors { get; }
        public IClass? BaseClass => (IClass?) _baseClass?.Value;

        public ReflectionClassBase(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
            if (type.BaseType != null)
            {
                _baseClass = typeFormat[type.BaseType!];
            }
            else
            {
                _baseClass = new Lazy<IType>(() => null);
            }
            
            Constructors = type.GetDeclaredConstructors().Select(constructor => new Constructor()
            {
                Parameters = constructor.GetParameters().Select(parameter =>
                    new LazyConstructorParameter(parameter.Name, typeFormat[parameter.ParameterType], parameter.HasDefaultValue, parameter.DefaultValue)),
                Visibility = constructor.GetVisibility()
            });
        }
    }
}