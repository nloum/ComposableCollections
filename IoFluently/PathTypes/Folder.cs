namespace IoFluently
{
    public partial class Folder : FolderOrMissingPath
    {
        public Folder(IFileOrFolderOrMissingPath path) : base(path)
        {
        }
    }
}