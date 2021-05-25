using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionNonGenericClass : INonGenericClass
    {
        public TypeIdentifier Identifier { get; }
        public Visibility Visibility { get; }
        public IReadOnlyList<IConstructor> Constructors { get; }
        public IReadOnlyList<IMethod> Methods { get; }
        public IReadOnlyList<IIndexer> Indexers { get; }
        public IReadOnlyList<IProperty> Properties { get; }

        public ReflectionNonGenericClass(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat)
        {
            Identifier = type.GetTypeIdentifier();
            Visibility = type.GetTypeVisibility();
            Properties = type.GetProperties().Where(property => property.GetIndexParameters().Length == 0)
                .ToImmutableList()
                .Select(property => new LazyProperty(typeFormat[property.PropertyType], property.Name, Visibility.Public));
        }
    }
}