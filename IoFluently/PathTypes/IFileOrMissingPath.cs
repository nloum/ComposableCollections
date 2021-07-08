using SimpleMonads;

namespace IoFluently
{
    public partial interface IFileOrMissingPath : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<File, MissingPath>,
        IFileOrFolderOrMissingPath
    {
        File ExpectFile();
        MissingPath ExpectMissingPath();
    }
}