using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public abstract class ReflectionComplexTypeBase : IComplexType
    {
        public Type Type { get; }
        public TypeIdentifier Identifier { get; }
        public Visibility Visibility { get; }
        public IReadOnlyList<IMethod> Methods { get; }
        public IReadOnlyList<IIndexer> Indexers { get; }
        public IReadOnlyList<IProperty> Properties { get; }
        public IReadOnlyList<IType> Interfaces { get; }

        protected ReflectionComplexTypeBase(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat)
        {
            Type = type;
            Identifier = type.GetTypeIdentifier();
            Visibility = type.GetTypeVisibility();
            Properties = type.GetProperties().Where(property => property.GetIndexParameters().Length == 0)
                .ToImmutableList()
                .Select(property => new LazyProperty(typeFormat[property.PropertyType], property.Name, Visibility.Public));
            Methods = type.GetMethods()
                .ToImmutableList()
                .Select(method => new LazyMethod(typeFormat[method.ReturnType], method.Name, Visibility.Public, method.GetParameters().Select(parameter => new LazyParameter(parameter.Name, typeFormat[parameter.ParameterType], parameter.IsOut, parameter.HasDefaultValue, parameter.DefaultValue))));
            Indexers = type.GetProperties().Where(property => property.GetIndexParameters().Length > 0)
                .ToImmutableList()
                .Select(property => new LazyIndexer(typeFormat[property.PropertyType], Visibility.Public, property.GetIndexParameters().Select(parameter => new LazyParameter(parameter.Name, typeFormat[parameter.ParameterType], false, parameter.HasDefaultValue, parameter.DefaultValue))));
            Interfaces = type.GetInterfaces()
                .Select(type => typeFormat[type])
                .Select(type => type.Value);
        }
    }
}