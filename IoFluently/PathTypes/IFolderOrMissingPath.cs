using SimpleMonads;

namespace IoFluently
{
    public partial interface IFolderOrMissingPath : SubTypesOf<IHasAbsolutePath>.IEither<Folder, MissingPath>,
        IFileOrFolderOrMissingPath,
        IHasAbsolutePath
    {
        Folder ExpectFolder();
        MissingPath ExpectMissingPath();
    }
}