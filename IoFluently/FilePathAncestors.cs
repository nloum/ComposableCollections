namespace IoFluently
{
    public class FilePathAncestors : Ancestors<FolderPath>
    {
        public FilePathAncestors(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        protected override FolderPath Expect(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath)
        {
            return fileOrFolderOrMissingPath.ExpectFolder();
        }
    }
}