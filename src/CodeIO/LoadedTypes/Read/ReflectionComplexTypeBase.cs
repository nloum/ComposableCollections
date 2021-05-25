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
        public TypeIdentifier Identifier { get; }
        public Visibility Visibility { get; }
        public IReadOnlyList<IMethod> Methods { get; }
        public IReadOnlyList<IIndexer> Indexers { get; }
        public IReadOnlyList<IProperty> Properties { get; }
        public IReadOnlyList<IType> Interfaces { get; }

        protected ReflectionComplexTypeBase(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat)
        {
            Identifier = type.GetTypeIdentifier();
            Visibility = type.GetTypeVisibility();
            Properties = type.GetProperties().Where(property => property.GetIndexParameters().Length == 0)
                .ToImmutableList()
                .Select(property => new LazyProperty(typeFormat[property.PropertyType], property.Name, Visibility.Public));
            Interfaces = type.GetInterfaces()
                .Select(type => typeFormat[type])
                .Select(type => type.Value);
        }
    }
}