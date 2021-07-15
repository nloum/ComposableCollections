using SimpleMonads;

namespace IoFluently
{
    public partial interface IFileOrFolderPath : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath>, IFileOrFolderOrMissingPath
    {
        
    }
}