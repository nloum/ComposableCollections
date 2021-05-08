using System;
using System.Collections.Immutable;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;

namespace DebuggableSourceGenerators
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
            return new(() => _lazyTypes.GetValue(key).Value);
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