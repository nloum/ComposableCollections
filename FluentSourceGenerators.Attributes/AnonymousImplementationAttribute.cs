using System;

namespace FluentSourceGenerators.Attributes
{
    public class AnonymousImplementationAttribute : Attribute
    {
        public AnonymousImplementationAttribute(Type[] allowedArguments, bool cachePropertyValues = false)
        {
            AllowedArguments = allowedArguments;
            CachePropertyValues = cachePropertyValues;
        }

        public Type[] AllowedArguments { get; }
        public bool CachePropertyValues { get; }
    }
}
