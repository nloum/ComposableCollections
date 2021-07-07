using System;

namespace DebuggableSourceGenerators.Read
{
    public interface ITypeRegistryService
    {
        Lazy<TypeBase> GetType(TypeIdentifier identifier);
        TypeBase TryAddType(TypeIdentifier identifier, Func<TypeBase> type);
    }
}