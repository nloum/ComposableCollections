using System;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators.Read
{
    public interface IStructuredType : TypeBase
    {
        ImmutableList<Property> Properties { get; }
        ImmutableList<Method> Methods { get; }
        ImmutableList<Indexer> Indexers { get; }
        ImmutableList<Lazy<Interface>> Interfaces { get; }
    }
}