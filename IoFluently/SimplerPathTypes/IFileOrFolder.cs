using SimpleMonads;

namespace IoFluently
{
    public interface IFileOrFolder : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IFolder>, IFileOrFolderOrMissingPath
    {
        
    }
}