using SimpleMonads;

namespace IoFluently
{
    public interface IFileOrFolder : SubTypesOf<IHasAbsolutePath>.IEither<File, Folder>,
        IFileOrFolderOrMissingPath
    {
    }
}