namespace IoFluently
{
    public partial class Folder : FolderOrMissingPath
    {
        public Folder(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        public AbsolutePathChildren Children => new AbsolutePathChildren(this, "*", IoService);
        public AbsolutePathDescendants Descendants => new AbsolutePathDescendants(this, "*", IoService);

        public AbsolutePathChildren GetChildren(string pattern) => new AbsolutePathChildren(this, pattern, IoService);
        public AbsolutePathDescendants GetDescendants(string pattern) => new AbsolutePathDescendants(this, pattern, IoService);
    }
}