using SimpleMonads;

namespace IoFluently
{
    public interface IFileOrFolder : IFileOrFolder<File, Folder>, IFileOrFolderOrMissingPath
    {
    }

    public interface IFileOrFolder<out TFile, out TFolder> : IEither<TFile, TFolder>,
        IFileOrFolderOrMissingPath<TFile, TFolder, MissingPath>
        where TFile : File
        where TFolder : Folder
    {
    }
}