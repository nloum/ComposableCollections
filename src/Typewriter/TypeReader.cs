using System;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;

namespace Typewriter
{
    public class TypeReader
    {
        public IComposableDictionary<TypeIdentifier, Lazy<IType>> LazyTypes { get; }
        public IComposableDictionary<TypeIdentifier, IType> Types { get; }

        public TypeReader()
        {
            LazyTypes = new ComposableDictionary<TypeIdentifier, Lazy<IType>>();
            Types = LazyTypes.WithMapping(x => x.Value, type => new Lazy<IType>(type));
        }
    }
}