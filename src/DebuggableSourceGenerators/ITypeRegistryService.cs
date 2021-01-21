using System;

namespace DebuggableSourceGenerators
{
    public interface ITypeRegistryService
    {
        Lazy<IType> GetType(string name, int arity = 0);
        IType TryAddType(string typeName, int arity, Func<IType> type);
        Lazy<IType> GetType(string namespaceName, string name, int arity = 0);
        IType TryAddType(string namespaceName, string typeName, int arity, Func<IType> type);
    }
}