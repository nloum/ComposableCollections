using System;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;

namespace DebuggableSourceGenerators.Read
{
    public class CodeIndexBuilder
    {
        private readonly IComposableDictionary<TypeIdentifier, Lazy<Type>> _lazyTypes;
        private readonly IComposableDictionary<TypeIdentifier, Type> _types;

        public CodeIndexBuilder()
        {
            _lazyTypes = new ComposableDictionary<TypeIdentifier, Lazy<Type>>();
            _types = _lazyTypes.WithMapping(x => x.Value, x => new Lazy<Type>(x));
        }

        public int Count => _lazyTypes.Count;

        public Lazy<Type> GetType(TypeIdentifier key)
        {
            return new(() =>
            {
                if (_lazyTypes.TryGetValue(key, out var value))
                {
                    return value.Value;
                }

                if (string.IsNullOrWhiteSpace(key.Namespace))
                {
                    var matchingTypeIdentifiers = _lazyTypes.Keys.Where(x => x.Name == key.Name).ToImmutableList();
                    if (matchingTypeIdentifiers.Count == 1)
                    {
                        return _lazyTypes[matchingTypeIdentifiers[0]].Value;
                    }

                    if (matchingTypeIdentifiers.Count > 1)
                    {
                        matchingTypeIdentifiers =
                            matchingTypeIdentifiers.Where(x => x.Arity == key.Arity).ToImmutableList();
                        
                        if (matchingTypeIdentifiers.Count == 1)
                        {
                            return _lazyTypes[matchingTypeIdentifiers[0]].Value;
                        }

                        if (matchingTypeIdentifiers.Count > 1)
                        {
                            throw new InvalidOperationException($"Multiple types match {key}");
                        }
                    }
                }
                
                throw new InvalidOperationException($"No types match {key}");
            });
        }

        public Lazy<Type> GetOrAdd(TypeIdentifier key, Func<Type> value)
        {
            var lazyValue = new Lazy<Type>(value);
            if (_lazyTypes.TryAdd(key, lazyValue))
            {
                return lazyValue;
            }
            else
            {
                return _lazyTypes[key];
            }
        }

        public CodeIndex Build()
        {
            return new CodeIndex(_lazyTypes);
        }
    }
}