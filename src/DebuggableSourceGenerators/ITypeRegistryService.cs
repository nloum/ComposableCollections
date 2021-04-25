using System;

namespace DebuggableSourceGenerators
{
    public interface ITypeRegistryService
    {
        Lazy<Type> GetType(TypeIdentifier identifier);
        Type TryAddType(TypeIdentifier identifier, Func<Type> type);
    }
}