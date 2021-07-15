using SimpleMonads;

namespace IoFluently
{
    public partial interface IFileOrFolder : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath>, IFileOrFolderOrMissingPath
    {
        
    }
}