using SimpleMonads;

namespace IoFluently
{
    public partial interface IFileOrMissingPath : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IMissingPath>, IFileOrFolderOrMissingPath
    {
        
    }
}