using SimpleMonads;

namespace IoFluently
{
    public interface IFolderOrMissingPath : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFolder, IMissingPath>, IFileOrFolderOrMissingPath
    {
        
    }
}