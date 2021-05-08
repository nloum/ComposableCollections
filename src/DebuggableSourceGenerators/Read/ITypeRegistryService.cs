using System;

namespace DebuggableSourceGenerators.Read
{
    public interface ITypeRegistryService
    {
        Lazy<Type> GetType(TypeIdentifier identifier);
        Type TryAddType(TypeIdentifier identifier, Func<Type> type);
    }
}