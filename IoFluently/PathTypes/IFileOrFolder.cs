using SimpleMonads;

namespace IoFluently
{
    public interface IFileOrFolder : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<File, Folder>,
        IFileOrFolderOrMissingPath
    {
    }
}