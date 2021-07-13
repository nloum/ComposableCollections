using SimpleMonads;

namespace IoFluently
{
    public interface IFileOrMissingPath : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IMissingPath>, IFileOrFolderOrMissingPath
    {
        
    }
}