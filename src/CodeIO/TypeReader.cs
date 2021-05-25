using System;
using ComposableCollections;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;

namespace CodeIO
{
    public class TypeReader
    {
        private MultiTypeDictionary<object> _typeReaders = new MultiTypeDictionary<object>();

        public TypeReader AddTypeFormat<T>(Func<T, TypeIdentifier> typeIdentifier, Func<T, IComposableReadOnlyDictionary<T, Lazy<IType>>, Lazy<IType>> reader)
        {
            _typeReaders.TryAdd(() => new TypeFormat<T>
                {
                    TypeIdentifier = typeIdentifier,
                    Reader = reader,
                    LazyTypes = new AnonymousObservableDictionaryAdapter<T, Lazy<IType>>(
                        kvp => LazyTypes.Remove(typeIdentifier(kvp.Key)),
                            kvp => LazyTypes.Add(typeIdentifier(kvp.Key), kvp.Value))
                        .WithDefaultValue((key, state) => reader(key, state)),
                }, out var previousValue,
                out var result);
            return this;
        }

        public IComposableReadOnlyDictionary<T, Lazy<IType>> GetTypeFormat<T>()
        {
            if (!_typeReaders.TryGetValue(out TypeFormat<T> state))
            {
                throw new InvalidOperationException($"No type reader has been added for type {typeof(T).Name}");
            }

            return state.LazyTypes;
        }

        private class TypeFormat<T>
        {
            public Func<T, TypeIdentifier> TypeIdentifier { get; init; }
            public Func<T, IComposableReadOnlyDictionary<T, Lazy<IType>>, Lazy<IType>> Reader { get; init; }
            public IComposableReadOnlyDictionary<T, Lazy<IType>> LazyTypes { get; init; } 
        }
        
        public IComposableDictionary<TypeIdentifier, Lazy<IType>> LazyTypes { get; }
        public IComposableDictionary<TypeIdentifier, IType> Types { get; }
        
        public TypeReader()
        {
            LazyTypes = new ComposableDictionary<TypeIdentifier, Lazy<IType>>();
            Types = LazyTypes.WithMapping(x => x.Value, type => new Lazy<IType>(type));
        }
    }
}