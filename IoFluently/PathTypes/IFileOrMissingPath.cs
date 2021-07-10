using SimpleMonads;

namespace IoFluently
{
    public interface IFileOrMissingPath : IFileOrMissingPath<File, MissingPath>, IFileOrFolderOrMissingPath
    {
    }
    
    public partial interface IFileOrMissingPath<out TFile, out TMissingPath> : IEither<TFile, TMissingPath>,
        IFileOrFolderOrMissingPath<TFile, Folder, TMissingPath>
        where TFile : File where TMissingPath : MissingPath
    {
    }
}