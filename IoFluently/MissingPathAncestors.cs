namespace IoFluently
{
    public class MissingPathAncestors : Ancestors<FileOrFolderOrMissingPath>
    {
        public MissingPathAncestors(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        protected override FileOrFolderOrMissingPath Expect(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath)
        {
            return fileOrFolderOrMissingPath.ExpectFileOrFolderOrMissingPath();
        }
    }
}