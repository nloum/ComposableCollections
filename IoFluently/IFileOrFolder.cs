using SimpleMonads;

namespace IoFluently
{
    public partial interface IFileOrFolder : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IFolder>, IFileOrFolderOrMissingPath
    {
        
    }
}