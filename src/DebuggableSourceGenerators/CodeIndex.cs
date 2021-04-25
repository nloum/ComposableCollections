using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;

namespace DebuggableSourceGenerators
{
    public class CodeIndex : IEnumerable<IKeyValue<TypeIdentifier, Type>>
    {
        private readonly IComposableDictionary<TypeIdentifier, Lazy<Type>> _lazyTypes;
        private readonly IComposableDictionary<TypeIdentifier, Type> _types;

        public CodeIndex(IComposableDictionary<TypeIdentifier, Lazy<Type>> lazyTypes)
        {
            _lazyTypes = lazyTypes;
            _types = _lazyTypes.WithMapping(x => x.Value, x => new Lazy<Type>(x));
        }

        public IEnumerator<IKeyValue<TypeIdentifier, Type>> GetEnumerator()
        {
            return _types.GetEnumerator();
        }

        public int Count => _types.Count;

        public Type GetValue(TypeIdentifier key)
        {
            return _types.GetValue(key);
        }

        public bool ContainsKey(TypeIdentifier key)
        {
            return _types.ContainsKey(key);
        }

        public IMaybe<Type> TryGetValue(TypeIdentifier key)
        {
            return _types.TryGetValue(key);
        }

        public IEnumerable<TypeIdentifier> Keys => _types.Keys;

        public IEnumerable<Type> Values => _types.Values;

        public bool TryGetValue(TypeIdentifier key, out Type value)
        {
            return _types.TryGetValue(key, out value);
        }

        public Type this[TypeIdentifier key]
        {
            get => _types[key];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}