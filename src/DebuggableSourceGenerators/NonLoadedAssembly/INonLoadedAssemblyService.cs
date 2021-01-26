using System;

namespace DebuggableSourceGenerators.NonLoadedAssembly
{
    public interface INonLoadedAssemblyService
    {
        void AddAllTypes(string assemblyFilePath);
        Lazy<IType> GetType(int rowId);
    }
}