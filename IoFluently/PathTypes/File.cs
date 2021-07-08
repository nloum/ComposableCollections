namespace IoFluently
{
    public partial class File : FileOrMissingPath
    {
        public File(IFileOrFolderOrMissingPath path) : base(path)
        {
        }
    }
}