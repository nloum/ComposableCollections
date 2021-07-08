using SimpleMonads;

namespace IoFluently
{
    public partial interface IFolderOrMissingPath : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<Folder, MissingPath>,
        IFileOrFolderOrMissingPath
    {
        Folder ExpectFolder();
        MissingPath ExpectMissingPath();
    }
}