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
        private readonly IComposableDictionary<TypeIdentifier, Lazy<TypeBase>> _lazyTypes;
        private readonly IComposableDictionary<TypeIdentifier, TypeBase> _types;

        public CodeIndexBuilder()
        {
            _lazyTypes = new ComposableDictionary<TypeIdentifier, Lazy<TypeBase>>();
            _types = _lazyTypes.WithMapping(x => x.Value, x => new Lazy<TypeBase>(x));
        }

        public int Count => _lazyTypes.Count;

        public Lazy<TypeBase> GetType(TypeIdentifier key)
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

        public Lazy<TypeBase> GetOrAdd(TypeIdentifier key, Func<TypeBase> value)
        {
            var lazyValue = new Lazy<TypeBase>(value);
            if (_lazyTypes.TryAdd(key, lazyValue))
            {
                return lazyValue;
            }
            else
            {
                return _lazyTypes[key];
            }
        }

        public Lazy<CodeIndex> LazyBuild()
        {
            return new Lazy<CodeIndex>(() => Build());
        }
        
        public CodeIndex Build()
        {
            return new CodeIndex(_lazyTypes);
        }
    }
}