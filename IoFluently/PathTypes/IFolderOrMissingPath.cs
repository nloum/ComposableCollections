using SimpleMonads;

namespace IoFluently
{
    public interface IFolderOrMissingPath : IFolderOrMissingPath<Folder, MissingPath>, IFileOrFolderOrMissingPath
    {
    }

    public partial interface IFolderOrMissingPath<out TFolder, out TMissingPath> : IEither<TFolder, TMissingPath>,
        IFileOrFolderOrMissingPath<File, TFolder, TMissingPath>
        where TFolder : Folder where TMissingPath : MissingPath
    {
    }
}