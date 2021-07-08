using SimpleMonads;

namespace IoFluently
{
    public partial interface IFileOrMissingPath : SubTypesOf<IHasAbsolutePath>.IEither<File, MissingPath>,
        IFileOrFolderOrMissingPath
    {
        File ExpectFile();
        MissingPath ExpectMissingPath();
    }
}