using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionStructure : IStructure, IReflectionType
    {
        public TypeIdentifier Identifier { get; }
        public Visibility Visibility { get; }
        public IReadOnlyList<IField> Fields { get; }
        public Type Type { get; }

        public ReflectionStructure(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat)
        {
            Type = type;
            Visibility = type.GetTypeVisibility();
            Identifier = type.GetTypeIdentifier();
            Fields = type.GetFields()
                .ToImmutableList()
                .Select(field => new LazyField(typeFormat[field.FieldType], field.Name, 
                    field.GetVisibility(), 
                    field.IsStatic));
        }
    }
}