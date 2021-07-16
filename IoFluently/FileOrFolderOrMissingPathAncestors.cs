namespace IoFluently
{
    public class FileOrFolderOrMissingPathAncestors : Ancestors<FileOrFolderOrMissingPath>
    {
        public FileOrFolderOrMissingPathAncestors(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        protected override FileOrFolderOrMissingPath Expect(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath)
        {
            return fileOrFolderOrMissingPath.ExpectFileOrFolderOrMissingPath();
        }
    }
}