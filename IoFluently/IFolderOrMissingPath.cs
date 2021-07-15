using SimpleMonads;

namespace IoFluently
{
    public partial interface IFolderOrMissingPath : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFolderPath, IMissingPath>, IFileOrFolderOrMissingPath
    {
        
    }
}