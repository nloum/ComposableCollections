using System;

namespace DebuggableSourceGenerators
{
    public interface ITypeRegistryService
    {
        Lazy<IType> GetType(TypeIdentifier identifier);
        IType TryAddType(TypeIdentifier identifier, Func<IType> type);
    }
}